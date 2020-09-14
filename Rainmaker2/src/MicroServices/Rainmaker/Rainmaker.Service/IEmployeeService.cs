using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rainmaker.Service
{
   public interface IEmployeeService : IServiceBase<Employee>
    {
          
       Task<List<Employee>> GetEmployeeEmailByRoleName(string roleName);
    }
}
