using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Models;

namespace TaskTracker.DAL.Repositories
{
    public class RoleRepository : Repository<Role>
    {
        public RoleRepository(IContext context) : base(context)
        {

        }
    }
}
