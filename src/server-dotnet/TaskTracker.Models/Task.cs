using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskTracker.Core.Architecture.Models;

namespace TaskTracker.Models
{
    public class Task : IModel
    {
        public int Id { get; set; }
        public int SprintId { get; set; }
        public int ClassificationId { get; set; }
        [StringLength(64)]
        public string Title { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        public int ExpectedHours { get; set; }
        public int ActualHours { get; set; }
        public bool IsCompleted { get; set; }


        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }


        public Sprint Sprint { get; set; }
        public Classification Classification { get; set; }
    }
}
