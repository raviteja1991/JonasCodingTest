using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using BusinessLayer.Services;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, IEmployeeRepository employeeRepository, IMapper mapper, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            _logger.LogInformation("Employee/GetAll API Call initiated.");
            try
            {
                var items = await _employeeService.GetAllEmployees();
                return _mapper.Map<IEnumerable<EmployeeDto>>(items);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error Info: " + ex.Message);
                return new List<EmployeeDto>();
            }
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<EmployeeDto> Get(string employeeCode)
        {
            _logger.LogInformation("Employee/Get API Call initiated.");
            try
            {
                var item = await _employeeService.GetEmployeeByCode(employeeCode);
                return _mapper.Map<EmployeeDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error Info: " + ex.Message);
                return new EmployeeDto();
            }
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<EmployeeDetails> GetEmployeeDetails(string employeeCode)
        {
            _logger.LogInformation("Employee/GetEmployeeDetails API Call initiated.");
            try
            {
                var item = await _employeeService.GetEmployeeDetails(employeeCode);
                return _mapper.Map<EmployeeDetails>(item);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error Info: " + ex.Message);
                return new EmployeeDetails();
            }
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody] string value, Employee employee)
        {
            _logger.LogInformation("Employee/Put API Call initiated.");

            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid Data");
            }
            try
            {
                bool item = await _employeeRepository.SaveEmployee(employee);
                if (item == true)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error Info: " + ex.Message);
                return NotFound();
            }            
        }

        // DELETE api/<controller>
        public void Delete(int id)
        {
        }
    }
}