using SMS.Business;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StockMarketSimulation.Api.Controllers
{
    public class AdviserController : ApiController
    {
        private AdviserBL oAdviserBL = new AdviserBL();

        [HttpGet]
        public List<AdviserDTO> GetAdviserData()
        {
            return oAdviserBL.GetAdviserData();
        }

        [HttpGet]
        public AdviserDTO AdviserSearchById(int AdviserId)
        {
            return oAdviserBL.AdviserSearchById(AdviserId);
        }

        [HttpGet]
        public AdviserDTO AdviserSearchByFilters(string adviserName, int adviserType)
        {
            return oAdviserBL.AdviserSearchByFilters(adviserName, adviserType);
        }

        [HttpPost]
        public bool InsertAdviserData(AdviserMaintanance oAdviserMaintanance)
        {
            return oAdviserBL.AdviserInsert(oAdviserMaintanance);
        }

        [HttpPost]
        public bool UpdateAdviserData(AdviserMaintanance oAdviserMaintanance)
        {
            return oAdviserBL.AdviserUpdate(oAdviserMaintanance);
        }

        [HttpPost]
        public bool DeleteAdviserData(AdviserDTO oCompanyDTO)
        {
            return oAdviserBL.AdviserDelete(oCompanyDTO);
        }

    }
}
