using EmployeeManagement.Persistence.IRepository;
using EmployeeManagement.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public EmployeeManager(IRepository<EmployeeMaster> employeeMaster)
        {
            _employeeMasterRepository = employeeMaster;
        }

        public IEnumerable<EmployeeMaster> GetEmployees()
        {
            return _employeeMasterRepository.GetAll();
        }

        public EmployeeMaster GetEmployeeById(int Id)
        {
            var empMaster= _employeeMasterRepository.GetById(Id);
            return empMaster;
        }

        public void CreateEmployee(EmployeeMaster employee)
        {
            _employeeMasterRepository.Add(employee);
        }

        public void EditEmployee(EmployeeMaster employee)
        {
            _employeeMasterRepository.Update(employee);
        }

        public void DeleteEmployee(EmployeeMaster employee)
        {
            _employeeMasterRepository.Delete(employee);
        }
    }
}

