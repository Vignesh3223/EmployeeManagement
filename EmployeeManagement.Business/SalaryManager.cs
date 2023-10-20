using EmployeeManagement.Persistence.IRepository;
using EmployeeManagement.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business
{
    public class SalaryManager
    {
        private readonly IRepository<SalaryMaster> _salaryMasterRepository;

        public SalaryManager(IRepository<SalaryMaster> salaryMaster)
        {
            _salaryMasterRepository = salaryMaster;
        }

        public IEnumerable<SalaryMaster> GetSalary()
        {
            return _salaryMasterRepository.GetAll();
        }
    }
}
