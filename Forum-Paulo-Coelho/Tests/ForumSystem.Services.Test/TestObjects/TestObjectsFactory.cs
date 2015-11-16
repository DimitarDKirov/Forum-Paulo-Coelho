using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Services.Test.TestObjects
{
    public static class TestObjectsFactory
    {
        public static MemoryRepository<User> GetUserRepository()
        {
            var usersRepository = new MemoryRepository<User>();
            for (int i = 0; i < 10; i++)
            {
                var user = new User
                {
                    Nickname = "Nick " + i,
                    Id="id"+i,
                    UserName="User"+i
                };

                usersRepository.Add(user);
            }

            return usersRepository;
        }
    }
}
