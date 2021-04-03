using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Models;

namespace TaskTracker.DAL.Repositories
{
    public class SprintRepository : Repository<Sprint>
    {
        public SprintRepository(IContext context) : base(context)
        {

        }
    }
}
