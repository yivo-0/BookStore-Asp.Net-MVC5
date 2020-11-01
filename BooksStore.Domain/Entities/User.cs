﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email{ get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
