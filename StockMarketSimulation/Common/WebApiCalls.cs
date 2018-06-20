using SMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Text;

namespace StockMarketSimulation.Common
{
    public class WebApiCalls
    {
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

        public bool InsertCompanyData(CompanyDTO oCompanyDTO)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Company/InsertCompanyData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);
                    var json = JsonConvert.SerializeObject(oCompanyDTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if(responce.IsSuccessStatusCode)
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

        public bool UpdateCompanyData(CompanyDTO oCompanyDTO)
        {
            bool ReturnValue = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = "Company/UpdateCompanyData";
                    client.BaseAddress = new Uri(GlobalValues.BaseUri);

                    var json = JsonConvert.SerializeObject(oCompanyDTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage responce = client.PostAsync(path, content).Result;
                    if(responce.IsSuccessStatusCode)
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
    }

}