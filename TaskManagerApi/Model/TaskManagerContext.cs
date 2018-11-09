using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerApi.Model
{
    public partial class TaskManagerContext : DbContext
    {       

        public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
            :base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Username)
                .IsUnique();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<DocumentInfo> DocumentInfos { get; set; }
    }
}
