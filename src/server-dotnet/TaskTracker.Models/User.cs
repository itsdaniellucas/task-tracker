using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskTracker.Core.Architecture.Models;

namespace TaskTracker.Models
{
    public class User : IModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        [StringLength(128)]
        public string Username { get; set; }
        [StringLength(128)]
        public string Password { get; set; } // hashed


        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }


        public Role Role { get; set; }
    }
}
