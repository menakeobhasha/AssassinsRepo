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
    public class PlayerController : ApiController
    {
        private PlayerBL oPlayerBL = new PlayerBL();

        [HttpGet]
        public List<PlayerDTO> GetPlayerData()
        {
            return oPlayerBL.GetPlayerData();
        }

        [HttpGet]
        public PlayerDTO PlayerSearchById(int PlayerId)
        {
            return oPlayerBL.PlayerSearchById(PlayerId);
        }

        [HttpPost]
        public bool InsertPlayerData(PlayerMaintanance oPlayerMaintanance)
        {
            return oPlayerBL.PlayerInsert(oPlayerMaintanance);
        }

        [HttpPost]
        public bool UpdatePlayerData(PlayerMaintanance oPlayerMaintanance)
        {
            return oPlayerBL.PlayerUpdate(oPlayerMaintanance);
        }

        [HttpPost]
        public bool DeletePlayerData(PlayerDTO oPlayerDTO)
        {
            return oPlayerBL.PlayerDelete(oPlayerDTO);
        }
    }
}
