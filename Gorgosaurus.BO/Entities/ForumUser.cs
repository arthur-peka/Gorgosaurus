﻿using Gorgosaurus.BO.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.BO.Entities
{
    public class ForumUser : BaseEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public bool IsAdmin { get; set; }
    }
}
