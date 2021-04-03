using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskTracker.Core.Architecture.Models;

namespace TaskTracker.Models
{
    public class Classification : IModel, IModelName
    {
        public int Id { get; set; }
        [StringLength(32)]
        public string Name { get; set; }


        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
