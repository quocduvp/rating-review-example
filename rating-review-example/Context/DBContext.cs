using rating_review_example.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace rating_review_example.Context
{
    public class DBContext : System.Data.Entity.DbContext
    {
        public DBContext(): base("name=DBContext")
        {
        }

        public DbSet<PassCode> PassCode { get; set; }

        public DbSet<Service> Service { get; set; }

        public DbSet<ScoreValue> ScoreValue { get; set; }

        public DbSet<Review> Review { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassCode>().ToTable("PassCode").HasRequired<Service>(s => s.Service).WithMany(p => p.PassCodes).HasForeignKey<int>(s => s.ServiceId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<ScoreValue>().ToTable("ScoreValue");
            modelBuilder.Entity<Review>().ToTable("Review").HasRequired<Service>(s => s.Service).WithMany(p => p.Reviews).HasForeignKey<int>(s => s.ServiceId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Review>().ToTable("Review").HasRequired<PassCode>(s => s.PassCode).WithMany(p => p.Reviews).HasForeignKey<int>(s => s.PassCodeId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Review>().ToTable("Review").HasRequired<ScoreValue>(s => s.Score).WithMany(p => p.Reviews).HasForeignKey<int>(s => s.ScoreId).WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}