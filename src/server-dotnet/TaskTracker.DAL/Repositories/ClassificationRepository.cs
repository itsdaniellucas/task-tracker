using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Models;

namespace TaskTracker.DAL.Repositories
{
    public class ClassificationRepository : Repository<Classification>
    {
        public ClassificationRepository(IContext context) : base(context)
        {

        }
    }
}
