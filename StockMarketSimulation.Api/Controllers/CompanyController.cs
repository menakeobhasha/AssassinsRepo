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
        // GET: Company
        [HttpGet]
        public List<CompanyDTO> GetCompanyData()
        {
            return oCompanyBL.GetCompanyData();
        }

        [HttpPost]
        public bool InsertCompanyData(CompanyDTO oCompanyDTO)
        {
            return oCompanyBL.CompanyInsert(oCompanyDTO);
        }

        [HttpPost]
        public bool UpdateCompanyData(CompanyDTO oCompanyDTO)
        {
            return oCompanyBL.CompanyInsert(oCompanyDTO);
        }

        //[HttpGet]
        //public String Get()
        //{
        //    return "Herllo";
        //}
    }
}