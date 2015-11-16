using ForumSystem.Data;
using ForumSystem.Models;
using ForumSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoriesRepository;

        public CategoriesService(IRepository<Category> categories)
        {
            this.categoriesRepository = categories;
        }

        public IQueryable<Category> GetAll()
        {
            return this.categoriesRepository.All();
        }

        public int Add(string name)
        {
            var newCategory = new Category
            {
                Name = name
            };

            this.categoriesRepository.Add(newCategory);
            this.categoriesRepository.SaveChanges();
            return newCategory.Id;
        }

        public void Update(int categoryId, string name)
        {
            var category = this.categoriesRepository.GetById(categoryId);
            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            category.Name = name;
            this.categoriesRepository.Update(category);
            this.categoriesRepository.SaveChanges();
        }
    }
}
