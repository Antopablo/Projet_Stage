using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    public class ApplicationDatabase : DbContext
    {
        public DbSet<File> FilesDatabase { get; set; }
        public DbSet<User> UsersDatabase { get; set; }

    }
}
