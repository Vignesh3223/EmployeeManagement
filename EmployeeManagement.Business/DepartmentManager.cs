using EmployeeManagement.Persistence.IRepository;
using EmployeeManagement.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business
{
    public class DepartmentManager
    {
        private readonly IRepository<DepartmentMaster> _departmentMasterRepository;

        public DepartmentManager(IRepository<DepartmentMaster> departmentMaster)
        {
            _departmentMasterRepository = departmentMaster;
        }

        public IEnumerable<DepartmentMaster> GetDepartments()
        {
            return _departmentMasterRepository.GetAll();
        }
    }
}
