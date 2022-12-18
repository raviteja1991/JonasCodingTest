using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbWrapper<Company> _companyDbWrapper;

        public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
        {
            _companyDbWrapper = companyDbWrapper;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await Task.FromResult(_companyDbWrapper.FindAll());
        }

        public async Task<Company> GetByCode(string companyCode)
        {
            return await Task.FromResult(_companyDbWrapper.Find(t => t.CompanyCode.Equals(companyCode))?.FirstOrDefault());
        }

        public async Task<bool> SaveCompany(Company company)
        {
            try
            {
                var itemRepo = _companyDbWrapper.Find(t =>
                    t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode))?.FirstOrDefault();
                if (itemRepo != null)
                {
                    itemRepo.CompanyName = company.CompanyName;
                    itemRepo.AddressLine1 = company.AddressLine1;
                    itemRepo.AddressLine2 = company.AddressLine2;
                    itemRepo.AddressLine3 = company.AddressLine3;
                    itemRepo.Country = company.Country;
                    itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                    itemRepo.FaxNumber = company.FaxNumber;
                    itemRepo.PhoneNumber = company.PhoneNumber;
                    itemRepo.PostalZipCode = company.PostalZipCode;
                    itemRepo.LastModified = company.LastModified;
                    return _companyDbWrapper.Update(itemRepo);
                }

                return await Task.FromResult(_companyDbWrapper.Insert(company));
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
