using System.Configuration;
using System.Data.SqlClient;

namespace StockMarketSimulation
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            BlackLion.Common.EnableLogger = true;

            BlackLion.Common.LogFilePath = ConfigurationManager.AppSettings["LogFilePath"];
            BlackLion.Common.ConnectionString = ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString;
            
        }

        void Application_End()
        {
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString);
        }
    }
}
