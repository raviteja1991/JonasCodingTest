using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;
        private readonly IDbWrapper<EmployeeData> _empDbWrapper;
        private readonly IDbWrapper<Company> _companyDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper, IDbWrapper<EmployeeData> empDbWrapper, IDbWrapper<Company> companyDbWrapper)
        {
            _employeeDbWrapper = employeeDbWrapper;
            _empDbWrapper = empDbWrapper;
            _companyDbWrapper = companyDbWrapper;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await Task.FromResult(_employeeDbWrapper.FindAll());
        }

        public async Task<Employee> GetByCode(string employeeCode)
        {
            return await Task.FromResult(_employeeDbWrapper.Find(t => t.EmployeeCode.Equals(employeeCode))?.FirstOrDefault());
        }

        public async Task<EmployeeData> GetDetails(string employeeCode)
        {
            var tempData = new EmployeeData();
            try
            {
                var itemEmployee = await Task.FromResult(_empDbWrapper.Find(t => t.EmployeeCode.Equals(employeeCode))?.FirstOrDefault());
                var itemComName = _companyDbWrapper.Find(t => t.CompanyCode.Equals(itemEmployee.CompanyCode))?.FirstOrDefault();

                if (itemEmployee != null)
                {
                    tempData.EmployeeCode = itemEmployee.EmployeeCode;
                    tempData.EmployeeName = itemEmployee.EmployeeName;
                    tempData.CompanyName = itemComName.CompanyName;
                    tempData.OccupationName = itemEmployee.OccupationName;
                    tempData.EmployeeStatus = itemEmployee.EmployeeStatus;
                    tempData.EmailAddress = itemEmployee.EmailAddress;
                    tempData.PhoneNumber = itemEmployee.PhoneNumber;
                    tempData.LastModifiedDateTime = itemEmployee.LastModifiedDateTime;
                }
                return tempData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> SaveEmployee(Employee employee)
        {
            try
            {
                var itemEmp = _employeeDbWrapper.Find(t =>
                    t.SiteId.Equals(employee.SiteId) && t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
                if (itemEmp != null)
                {
                    itemEmp.EmployeeName = employee.EmployeeName;
                    itemEmp.Occupation = employee.Occupation;
                    itemEmp.EmployeeStatus = employee.EmployeeStatus;
                    itemEmp.EmailAddress = employee.EmailAddress;
                    itemEmp.Phone = employee.Phone;
                    itemEmp.LastModified = employee.LastModified;
                    return _employeeDbWrapper.Update(itemEmp);
                }

                return await Task.FromResult(_employeeDbWrapper.Insert(employee));
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
