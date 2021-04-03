using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Models;

namespace TaskTracker.DAL.Repositories
{
    public class TaskRepository : Repository<Task>
    {
        public TaskRepository(IContext context) : base(context)
        {

        }
        protected override IQueryable<Task> AttachIncludes(DbSet<Task> dbSet)
        {
            return dbSet.Include(i => i.Sprint)
                        .Include(i => i.Classification);
        }
    }
}
