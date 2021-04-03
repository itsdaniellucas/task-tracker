using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTracker.Core.Architecture.Models
{
    public interface IModelName : IModel
    {
        string Name { get; set; } 
    }
}
