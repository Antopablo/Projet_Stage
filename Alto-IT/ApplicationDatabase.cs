﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    public class ApplicationDatabase : DbContext
    {
        public DbSet<User> UsersDatabase { get; set; }
        public DbSet<Exigence> ExigenceDatabase { get; set; }

        public DbSet<Norme> NormeDatabase { get; set; }
    }
}
