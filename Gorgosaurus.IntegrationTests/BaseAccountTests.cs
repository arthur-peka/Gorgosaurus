﻿using Gorgosaurus.BO.Entities;
using Gorgosaurus.Common;
using Gorgosaurus.DA.Repositories;
using Gorgosaurus.Models;
using Nancy.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.IntegrationTests
{
    public abstract class BaseAccountTests
    {
        protected void CreateUser(string username, string password, bool isAdmin, string name = "Tester", string surname = "Teserson", long id = 1)
        {
            string userSalt = "11111";

            string hash = CryptoHelper.GenerateHash(password, userSalt);
            UserRepository.Instance.Insert(new ForumUser()
            {
                Id = id,
                Username = username,
                Password = userSalt + hash,
                IsAdmin = isAdmin,
                Name = "Tester",
                Surname = "Testerson"
            });
        }

        protected BrowserResponse Login(string username, string password, Browser browser)
        {
            var resp = browser.Post("/account/login", with =>
            {
                with.HttpRequest();
                with.JsonBody<LoginModel>(new LoginModel() { Username = username, Password = password });
            });

            return resp;
        }
    }
}
