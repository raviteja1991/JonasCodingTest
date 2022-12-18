using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        ILogger<CompanyController> _logger;

        public CompanyController(ICompanyService companyService, ICompanyRepository companyRepository, IMapper mapper, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
        }
        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            _logger.LogInformation("Company/GetAll API Call initiated.");
            try
            {
                var items = await _companyService.GetAllCompanies();
                return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error Info: " + ex.Message);
                return new List<CompanyDto>();
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<CompanyDto> Get(string companyCode)
        {
            _logger.LogInformation("Company/Get API Call initiated.");
            try
            {
                var item = await _companyService.GetCompanyByCode(companyCode);
                return _mapper.Map<CompanyDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error Info: " + ex.Message);
                return new CompanyDto();
            }
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody] string value, Company company)
        {
            _logger.LogInformation("Company/Put API Call initiated.");

            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid Data");
            }
            try
            {
                bool item = await _companyRepository.SaveCompany(company);

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

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}