using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Services.Contracts
{
    public interface ICategoriesService
    {
        IQueryable<Category> GetAll();

        int Add(string name);

        void Update(int id, string name);
    }
}
