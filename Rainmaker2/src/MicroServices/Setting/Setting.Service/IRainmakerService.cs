using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Setting.Service
{
    public interface IRainmakerService
    {
        Task<List<Model.UserRole>> GetUserRoles(IEnumerable<string> authHeader);
        Task<bool> UpdateUserRoles(List<Model.UserRole> userRoles, IEnumerable<string> authHeader);
    }
}
