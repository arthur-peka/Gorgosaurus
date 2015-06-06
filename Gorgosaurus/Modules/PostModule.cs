﻿using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.ModelBinding;
using Gorgosaurus.BO.Entities;
using Gorgosaurus.DA.Repositories;

namespace Gorgosaurus.Modules
{
    public class PostModule : NancyModule
    {
        public PostModule()
        {
            Post["/post/add"] = parameters =>
            {
                var newForumPost = this.Bind<ForumPost>();

                ForumPostRepository.Instance.Insert(newForumPost, true);

                return true;
            };
        }

    }
}