using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Models;

namespace TaskTracker.DAL.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(IContext context) : base(context)
        {

        }
        protected override IQueryable<User> AttachIncludes(DbSet<User> dbSet)
        {
            return dbSet.Include(i => i.Role);
        }
    }
}
