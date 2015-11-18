namespace ForumSystem.Services.Test.TestObjects
{
    using ForumSystem.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MemoryRepository<T> : IRepository<T> where T : class
    {
        private IList<T> data;

        public MemoryRepository()
        {
            this.data = new List<T>();
            this.Attached = new List<T>();
            this.Detached = new List<T>();
            this.Updated = new List<T>();
        }

        public IList<T> Attached { get; set; }
        public IList<T> Detached { get; set; }
        public IList<T> Updated { get; set; }
        public bool IsDisposed { get; private set; }
        public int NumberOfSaves { get; private set; }

        public IQueryable<T> All()
        {
            return this.data.AsQueryable<T>();
        }

        public T GetById(object id)
        {
            int index;
            if (id is int)
            {
                index = (int)id;
            }
            else
            {
                var random = new Random();
                index = random.Next(this.data.Count);
            }

            if (index >= this.data.Count)
            {
                return null;
            }

            return this.data[index];
        }

        public void Add(T entity)
        {
            this.data.Add(entity);
        }

        public void Update(T entity)
        {
            this.Updated.Add(entity);
        }

        public void Delete(T entity)
        {
            this.data.Remove(entity);
        }

        public void Delete(object id)
        {
            int index = (int)id;
            if (index >= this.data.Count)
            {
                throw new InvalidOperationException("There are less elements than required");
            }

            this.data.RemoveAt(index);
        }

        public T Attach(T entity)
        {
            this.Attached.Add(entity);
            return entity;
        }

        public void Detach(T entity)
        {
            this.Detached.Add(entity);
        }

        public int SaveChanges()
        {
            this.NumberOfSaves++;
            return this.NumberOfSaves;
        }

        public void Dispose()
        {
            this.IsDisposed = true;
        }
    }
}
