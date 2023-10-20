using EmployeeManagement.Persistence.IRepository;
using EmployeeManagement.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business
{
    public class DesignationManager
    {
        private readonly IRepository<DesignationMaster> _designationMasterRepository;

        public DesignationManager(IRepository<DesignationMaster> designationMaster)
        {
            _designationMasterRepository = designationMaster;
        }

        public IEnumerable<DesignationMaster> GetDesignation()
        {
            return _designationMasterRepository.GetAll();
        }
    }
}
