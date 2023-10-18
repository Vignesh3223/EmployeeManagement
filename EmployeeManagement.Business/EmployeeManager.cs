using EmployeeManagement.Persistence.IRepository;
using EmployeeManagement.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business
{
    public class EmployeeManager
    {
        private readonly IRepository<EmployeeMaster> _employeeMasterRepository;
        public EmployeeManager(IRepository<EmployeeMaster> employeeMasterRepository)
        {
            _employeeMasterRepository = employeeMasterRepository;
        }
    }
}
