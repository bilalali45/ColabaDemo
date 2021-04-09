using System;
using System.Collections.Generic;
using System.Text;

namespace Rainmaker.Model
{
    public class ErrorModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
    public class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsRoleAssigned { get; set; }
    }
}
