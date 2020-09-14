using Microsoft.EntityFrameworkCore;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using System.Linq;

namespace Rainmaker.Service
{
    public class EmployeeService : ServiceBase<RainMakerContext, Employee>, IEmployeeService
    {
        public EmployeeService(IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services)
          : base(previousUow, services)
        {

        }
        public async Task<List<Employee>> GetEmployeeEmailByRoleName(string roleName)
        {
            return await Uow.Repository<Employee>().Query(x=>x.UserProfile.UserInRoles.Any(y=>y.UserRole.RoleName.ToLower()==roleName.ToLower()))
                 .Include(x => x.UserProfile).ThenInclude(x => x.UserInRoles).ThenInclude(x => x.UserRole)
                 .Include(x => x.UserProfile).ThenInclude(x => x.Employees).ThenInclude(x => x.EmployeeBusinessUnitEmails).ThenInclude(x => x.EmailAccount)
                 .ToListAsync();
        }
    }

}
