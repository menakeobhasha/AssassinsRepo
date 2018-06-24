using SMS.Business;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace StockMarketSimulation.Api.Controllers
{
    
    public class CompanyController : ApiController
    {
        private CompanyBL oCompanyBL = new CompanyBL();
        
        [HttpGet]
        public List<CompanyDTO> GetCompanyData()
        {
            return oCompanyBL.GetCompanyData();
        }

        [HttpGet]
        [ActionName("CompanySearchByFilters")]
        public CompanyDTO CompanySearchByFilters(string companyName,int companyType)
        {
            return oCompanyBL.CompanySearchByFilters(companyName, companyType);
        }

        [HttpGet]
        [ActionName("CompanySearchById")]
        public CompanyDTO CompanySearchById(int CompanyId)
        {
            return oCompanyBL.CompanySearchById(CompanyId);
        }

        [HttpPost]
        [ActionName("InsertCompanyData")]
        public bool InsertCompanyData(CompanyMaintanance oCompanyMaintanance)
        {
            return oCompanyBL.CompanyInsert(oCompanyMaintanance);
        }

        [HttpPost]
        [ActionName("UpdateCompanyData")]
        public bool UpdateCompanyData(CompanyMaintanance oCompanyMaintanance)
        {
            return oCompanyBL.CompanyUpdate(oCompanyMaintanance);
        }

        [HttpPost]
        [ActionName("DeleteCompanyData")]
        public bool DeleteCompanyData(CompanyDTO oCompanyDTO)
        {
            return oCompanyBL.CompanyDelete(oCompanyDTO);
        }

        //[HttpGet]
        //public CompanyDTO Get(int CompnayId)
        //{
        //    //return "Herllo";
        //    return oCompanyBL.CompanySearchById(CompnayId);
        //}
    }
}