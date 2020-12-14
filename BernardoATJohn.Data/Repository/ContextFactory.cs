using BernardoATJohn.MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace BernardoATJohn.Data.Repository
{
    public class ContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BernardoATJohn_Local; Trusted_Connection = True; MultipleActiveResultSets = true");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
