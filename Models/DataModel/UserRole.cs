using System;
using System.ComponentModel.DataAnnotations;

namespace RestApiServer.Models.DataModel
{
    public class UserRole
    {
        [Key]
        public string RoleID { get; set; }

        public string RoleName { get; set; }

        public byte RolePriority { get; set; }

        public DateTime ModifiedUtcDate { get; set; }

        public virtual ICollection<UserRolesByUser> UserRolesByUsers { get; set; }
    }
}

