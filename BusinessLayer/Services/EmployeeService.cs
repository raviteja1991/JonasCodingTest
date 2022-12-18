using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees()
        {
            var res = await _employeeRepository.GetAll();
            if (res != null)
                return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
            else
                return null;
        }

        public async Task<EmployeeInfo> GetEmployeeByCode(string employeeCode)
        {
            var result = await _employeeRepository.GetByCode(employeeCode);
            if (result != null)
                return _mapper.Map<EmployeeInfo>(result);
            else
                return null;
        }

        public async Task<EmployeeInformation> GetEmployeeDetails(string employeeCode)
        {
            var result = await _employeeRepository.GetDetails(employeeCode);
            if (result != null)
                return _mapper.Map<EmployeeInformation>(result);
            else
                return null;
        }
    }
}