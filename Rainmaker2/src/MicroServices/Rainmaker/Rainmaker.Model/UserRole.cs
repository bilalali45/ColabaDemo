using System;
using System.Collections.Generic;
using System.Text;

namespace Rainmaker.Model
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsRoleAssigned { get; set; }
    }
}
