using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Core.Architecture.Models;
using TaskTracker.Models;
using System.Diagnostics;
using TaskModel = TaskTracker.Models.Task;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.DAL
{
    public class TaskTrackerContext : DbContext, IContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Classification> Classifications { get; set; }

        public TaskTrackerContext()
            : base(GetContextOptions())
        {
            Database.SetCommandTimeout(300);
        }

        private static DbContextOptions GetContextOptions()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            var options = new DbContextOptionsBuilder();
            options.UseSqlServer(connectionString);
            return options.Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateNow = DateTime.Now;

            modelBuilder.Entity<Role>().HasData(new Role[]
            {
                new Role() { Id = 1, Name = "Admin", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Role() { Id = 2, Name = "User", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
            });

            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User() { Id = 1, Username = "admin", Password = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", RoleId = 1, DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new User() { Id = 2, Username = "user", Password = "04f8996da763b7a969b1028ee3007569eaf3a635486ddab211d512c85b9df8fb", RoleId = 2, DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
            });

            modelBuilder.Entity<Classification>().HasData(new Classification[]
            {
                new Classification() { Id = 1, Name = "Backlog", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Classification() { Id = 2, Name = "Active", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Classification() { Id = 3, Name = "Closed", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
                new Classification() { Id = 4, Name = "Archived", DateCreated = dateNow, DateModified = dateNow, CreatedBy = 0, ModifiedBy = 0, IsActive = true },
            });
        }

        public void Commit()
        {
            this.SaveChanges();
        }

        public Task CommitAsync()
        {
            return this.SaveChangesAsync();
        }

        public DbContext GetContext()
        {
            return this;
        }

        public void RunTransaction(Action transaction)
        {
            using (var context = this)
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        transaction.Invoke();
                        context.Commit();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task RunTransactionAsync(Func<Task> transaction)
        {
            using (var context = this)
            {
                using (var trans = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await transaction.Invoke();
                        await context.CommitAsync();
                        await trans.CommitAsync();
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex);
                        await trans.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public DbSet<T> Get<T>() where T : class, IModel
        {
            return this.Set<T>();
        }
    }
}
