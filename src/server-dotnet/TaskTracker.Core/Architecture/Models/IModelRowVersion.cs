using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTracker.Core.Architecture.Models
{
    public interface IModelRowVersion : IModel
    {
        byte[] RowVersion { get; set; }
    }
}
