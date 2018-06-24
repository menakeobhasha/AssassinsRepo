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
    public class BrokerController : ApiController
    {
        private BrokerBL oBrokerBL = new BrokerBL();

        [HttpGet]
        public List<BrokerDTO> GetBrokerData()
        {
            return oBrokerBL.GetBrokerData();
        }

        [HttpGet]
        public BrokerDTO BrokerSearchById(int BrokerId)
        {
            return oBrokerBL.BrokerSearchById(BrokerId);
        }

        [HttpPost]
        public bool InsertBrokerData(BrokerMaintanance oBrokerMaintanance)
        {
            return oBrokerBL.BrokerInsert(oBrokerMaintanance);
        }

        [HttpPost]
        public bool UpdateBrokerData(BrokerMaintanance oBrokerMaintanance)
        {
            return oBrokerBL.BrokerUpdate(oBrokerMaintanance);
        }

        [HttpPost]
        public bool DeleteBrokerData(BrokerDTO oBrokerDTO)
        {
            return oBrokerBL.BrokerDelete(oBrokerDTO);
        }
    }
}
