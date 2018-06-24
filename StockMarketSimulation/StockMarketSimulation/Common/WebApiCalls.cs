using Newtonsoft.Json;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace StockMarketSimulation.Common
{
    public class WebApiCalls
    {
        #region Company
        public List<CompanyDTO> GetCompanyData()
        {
            List<CompanyDTO> oCompanyDTO = new List<CompanyDTO>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Company/GetCompanyData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);

                    HttpResponseMessage responce = client.GetAsync(path).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var value = responce.Content.ReadAsStringAsync().Result;
                        oCompanyDTO = JsonConvert.DeserializeObject<List<CompanyDTO>>(value);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return oCompanyDTO;
        }

        public CompanyDTO CompanySearchById(int CompanyId)
        {
            CompanyDTO oCompanyDTO = new CompanyDTO();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Company/CompanySearchById?CompanyId=" + CompanyId;
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);

                    HttpResponseMessage responce = client.GetAsync(path).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var value = responce.Content.ReadAsStringAsync().Result;
                        oCompanyDTO = JsonConvert.DeserializeObject<CompanyDTO>(value);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return oCompanyDTO;
        }

        public bool InsertCompanyData(CompanyMaintanance oCompanyMaintanance)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Company/InsertCompanyData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    var json = JsonConvert.SerializeObject(oCompanyMaintanance);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        string jsnString = responce.Content.ReadAsStringAsync().Result;
                        ReturnValue = JsonConvert.DeserializeObject<bool>(jsnString);
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            return ReturnValue;
        }

        public bool UpdateCompanyData(CompanyMaintanance oCompanyMaintanance)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Company/UpdateCompanyData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);

                    var json = JsonConvert.SerializeObject(oCompanyMaintanance);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        string jsnString = responce.Content.ReadAsStringAsync().Result;
                        ReturnValue = JsonConvert.DeserializeObject<bool>(jsnString);
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            return ReturnValue;
        }        

        public bool DeleteCompanyData(CompanyDTO oCompanyDTO)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Company/DeleteCompanyData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);

                    var json = JsonConvert.SerializeObject(oCompanyDTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        string jsnString = responce.Content.ReadAsStringAsync().Result;
                        ReturnValue = JsonConvert.DeserializeObject<bool>(jsnString);
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            return ReturnValue;
        } 
        #endregion

        public LoginDTO GetLoginData(string UserName)
        {
            LoginDTO oLoginDTO = new LoginDTO();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Login/GetLoginData?UserName=" + UserName;
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);

                    HttpResponseMessage responce = client.GetAsync(path).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var value = responce.Content.ReadAsStringAsync().Result;
                        oLoginDTO = JsonConvert.DeserializeObject<LoginDTO>(value);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return oLoginDTO;
        }

        #region Player
        public List<PlayerDTO> GetPlayerData()
        {
            List<PlayerDTO> oPlayerDTO = new List<PlayerDTO>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Player/GetPlayerData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    HttpResponseMessage responce = client.GetAsync(path).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var value = responce.Content.ReadAsStringAsync().Result;
                        oPlayerDTO = JsonConvert.DeserializeObject<List<PlayerDTO>>(value);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return oPlayerDTO;
        }

        public PlayerDTO PlayerSearchById(int PlayerId)
        {
            PlayerDTO oPlayerDTO = new PlayerDTO();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Player/PlayerSearchById?PlayerId=" + PlayerId;
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);

                    HttpResponseMessage responce = client.GetAsync(path).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var value = responce.Content.ReadAsStringAsync().Result;
                        oPlayerDTO = JsonConvert.DeserializeObject<PlayerDTO>(value);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return oPlayerDTO;
        }

        public bool InsertPlayerData(PlayerMaintanance oPlayerMaintanance)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Player/InsertPlayerData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    var json = JsonConvert.SerializeObject(oPlayerMaintanance);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        string jsnString = responce.Content.ReadAsStringAsync().Result;
                        ReturnValue = JsonConvert.DeserializeObject<bool>(jsnString);
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            return ReturnValue;
        }

        public bool UpdatePlayerData(PlayerMaintanance oPlayerMaintanance)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Player/UpdatePlayerData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    var json = JsonConvert.SerializeObject(oPlayerMaintanance);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        string jsnString = responce.Content.ReadAsStringAsync().Result;
                        ReturnValue = JsonConvert.DeserializeObject<bool>(jsnString);
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            return ReturnValue;
        }

        public bool DeletePlayerData(PlayerDTO oPlayerDTO)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Player/DeletePlayerData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    var json = JsonConvert.SerializeObject(oPlayerDTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        string jsnString = responce.Content.ReadAsStringAsync().Result;
                        ReturnValue = JsonConvert.DeserializeObject<bool>(jsnString);
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            return ReturnValue;
        }
        #endregion

        #region Adviser
        public List<AdviserDTO> GetAdviserData()
        {
            List<AdviserDTO> oAdviserDTO = new List<AdviserDTO>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Adviser/GetAdviserData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    HttpResponseMessage responce = client.GetAsync(path).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var value = responce.Content.ReadAsStringAsync().Result;
                        oAdviserDTO = JsonConvert.DeserializeObject<List<AdviserDTO>>(value);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return oAdviserDTO;
        }

        public AdviserDTO AdviserSearchById(int AdviserId)
        {
            AdviserDTO oAdviserDTO = new AdviserDTO();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Adviser/AdviserSearchById?AdviserId=" + AdviserId;
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);

                    HttpResponseMessage responce = client.GetAsync(path).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var value = responce.Content.ReadAsStringAsync().Result;
                        oAdviserDTO = JsonConvert.DeserializeObject<AdviserDTO>(value);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return oAdviserDTO;
        }

        public AdviserDTO AdviserSearchByFilters(string adviserName, int adviserType)
        {
            AdviserDTO oAdviserDTO = new AdviserDTO();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Adviser/AdviserSearchByFilters?adviserName=" + adviserName + "&adviserType=" + adviserType;
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);

                    HttpResponseMessage responce = client.GetAsync(path).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var value = responce.Content.ReadAsStringAsync().Result;
                        oAdviserDTO = JsonConvert.DeserializeObject<AdviserDTO>(value);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return oAdviserDTO;
        }

        public bool InsertAdviserData(AdviserMaintanance oAdviserMaintanance)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Adviser/InsertAdviserData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    var json = JsonConvert.SerializeObject(oAdviserMaintanance);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        string jsnString = responce.Content.ReadAsStringAsync().Result;
                        ReturnValue = JsonConvert.DeserializeObject<bool>(jsnString);
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            return ReturnValue;
        }

        public bool UpdateAdviserData(AdviserMaintanance oAdviserMaintanance)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Adviser/UpdateAdviserData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    var json = JsonConvert.SerializeObject(oAdviserMaintanance);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        string jsnString = responce.Content.ReadAsStringAsync().Result;
                        ReturnValue = JsonConvert.DeserializeObject<bool>(jsnString);
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            return ReturnValue;
        }

        public bool DeleteAdviserData(AdviserDTO oAdviserDTO)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Adviser/DeleteAdviserData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    var json = JsonConvert.SerializeObject(oAdviserDTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        string jsnString = responce.Content.ReadAsStringAsync().Result;
                        ReturnValue = JsonConvert.DeserializeObject<bool>(jsnString);
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            return ReturnValue;
        }
        #endregion

        #region Broker
        public List<BrokerDTO> GetBrokerData()
        {
            List<BrokerDTO> oBrokerDTO = new List<BrokerDTO>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Broker/GetBrokerData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    HttpResponseMessage responce = client.GetAsync(path).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var value = responce.Content.ReadAsStringAsync().Result;
                        oBrokerDTO = JsonConvert.DeserializeObject<List<BrokerDTO>>(value);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return oBrokerDTO;
        }

        public BrokerDTO BrokerSearchById(int BrokerId)
        {
            BrokerDTO oBrokerDTO = new BrokerDTO();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Broker/BrokerSearchById?BrokerId=" + BrokerId;
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);

                    HttpResponseMessage responce = client.GetAsync(path).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var value = responce.Content.ReadAsStringAsync().Result;
                        oBrokerDTO = JsonConvert.DeserializeObject<BrokerDTO>(value);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return oBrokerDTO;
        }

        public bool InsertBrokerData(BrokerMaintanance oBrokerMaintanance)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Broker/InsertBrokerData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    var json = JsonConvert.SerializeObject(oBrokerMaintanance);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        string jsnString = responce.Content.ReadAsStringAsync().Result;
                        ReturnValue = JsonConvert.DeserializeObject<bool>(jsnString);
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            return ReturnValue;
        }

        public bool UpdateBrokerData(BrokerMaintanance oBrokerMaintanance)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Broker/UpdateBrokerData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    var json = JsonConvert.SerializeObject(oBrokerMaintanance);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        string jsnString = responce.Content.ReadAsStringAsync().Result;
                        ReturnValue = JsonConvert.DeserializeObject<bool>(jsnString);
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            return ReturnValue;
        }       

        public CompanyDTO CompanySearchByFilters(string companyName, int companyType)
        {
            CompanyDTO oCompanyDTO = new CompanyDTO();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Company/CompanySearchByFilters?companyName=" + companyName + "&companyType="+ companyType;
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);

                    HttpResponseMessage responce = client.GetAsync(path).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var value = responce.Content.ReadAsStringAsync().Result;
                        oCompanyDTO = JsonConvert.DeserializeObject<CompanyDTO>(value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oCompanyDTO;
        }

        public bool DeleteBrokerData(BrokerDTO oBrokerDTO)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Broker/DeleteBrokerData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    var json = JsonConvert.SerializeObject(oBrokerDTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        string jsnString = responce.Content.ReadAsStringAsync().Result;
                        ReturnValue = JsonConvert.DeserializeObject<bool>(jsnString);
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            return ReturnValue;
        }
        #endregion

        public string GetUserNameByUserIdFromAllUsers(string userId)
        {
            string userName = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Login/GetUserNameByUserIdFromAllUsers?userId=" + userId;
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);

                    HttpResponseMessage responce = client.GetAsync(path).Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var value = responce.Content.ReadAsStringAsync().Result;
                        userName = JsonConvert.DeserializeObject<string>(value);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return userName;
        }
    }

}