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
        [ActionName("CompanySearchById")]
        public CompanyDTO CompanySearchById(int CompanyId)
        {
            return oCompanyBL.CompanySearchById(CompanyId);
        }

        [HttpPost]
        [ActionName("InsertCompanyData")]
        public bool InsertCompanyData(CompanyDTO oCompanyDTO)
        {
            return oCompanyBL.CompanyInsert(oCompanyDTO);
        }

        [HttpPost]
        [ActionName("UpdateCompanyData")]
        public bool UpdateCompanyData(CompanyDTO oCompanyDTO)
        {
            return oCompanyBL.CompanyUpdate(oCompanyDTO);
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