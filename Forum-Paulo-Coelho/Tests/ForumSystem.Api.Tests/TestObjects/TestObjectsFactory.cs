﻿namespace ForumSystem.Api.Tests.TestObjects
{
    using ForumSystem.Models;
    using ForumSystem.Services.Contracts;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    class TestObjectsFactory
    {
        public static IThreadService GetThreadService()
        {
            var threadsService = new Mock<IThreadService>();

            threadsService.Setup(s => s.All()).Returns(new List<Thread>
            {
                new Thread(){
                    DateCreated = DateTime.Now.AddDays(2), Id = 1, Title = "Test Title 1", Content = "Test Content 1",
                    User = new User
                    {
                        UserName = "JhonDoe@abv.bg",
                        Id = Guid.NewGuid().ToString(),
                        Nickname = "JhonDoe",
                        Email = "JhonDoe@abv.bg"
                    }, Categories = new List<Category>()
                    {
                        new Category { Id = 2 }
                    }},
                new Thread(){
                    DateCreated = DateTime.Now.AddDays(4), Id = 2, Title = "Test Title 2", Content = "Test Content 2",
                    User = new User
                    {
                        UserName = "batman@abv.bg",
                        Id = Guid.NewGuid().ToString(),
                        Nickname = "Batman",
                        Email = "batman@abv.bg"
                    }, Categories = new List<Category>()
                    {
                        new Category { Id = 2 }
                    }},
                new Thread(){
                    DateCreated = DateTime.Now.AddDays(6), Id = 3, Title = "Test Title 3", Content = "Test Content 3",
                    User = new User
                        {
                            UserName = "DonchoMinkov@abv.bg",
                            Id = Guid.NewGuid().ToString(),
                            Nickname = "DonchoMinkov",
                            Email = "DonchoMinkov@abv.bg"
                        }, 
                    Categories = new List<Category>()
                        {
                            new Category { Id = 3 }
                        }}
            }.AsQueryable());

            threadsService.Setup(s => s.Add(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Thread
                {
                    Id = 1,
                    Title = "Test Title",
                    DateCreated = DateTime.Now,
                    Content = "Some Content",
                    User = new User
                    {
                        UserName = "batman@abv.bg",
                        Id = Guid.NewGuid().ToString(),
                        Nickname = "Batman",
                        Email = "batman@abv.bg"
                    }
                });

            return threadsService.Object;
        }
    }
}
