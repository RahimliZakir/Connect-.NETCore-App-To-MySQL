using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUI.Models.Entities;

namespace WebApplicationUI.Models.DataContexts
{
    public class ToDoListDbConext : DbContext
    {
        public ToDoListDbConext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Todo>().Property(e => e.CreatedDate).HasDefaultValueSql("(DATE_ADD(UTC_TIMESTAMP(), INTERVAL 4 HOUR))");
        }
    }


}
