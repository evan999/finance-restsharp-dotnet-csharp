using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestSharp;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;

// using System.Web.Script;

namespace FinanceScrape_RestSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            RestClient client = new RestClient("https://morning-star.p.rapidapi.com");

            RestRequest request = new RestRequest("/market/get-summary", Method.GET);
            request.AddHeader("x-rapidapi-host", "morning-star.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "fb96429169msh172f9e8369dbb6dp11b294jsn617d9bce3a4d");

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            dynamic stockDataJson = JsonConvert.DeserializeObject(content);


            JArray USAStockData = stockDataJson["MarketRegions"]["USA"];
            Console.WriteLine(USAStockData);
            


            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Finance;Integrated Security=True"
                + ";Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" 
                + "MultiSubnetFailover=False";

            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();


                foreach (JToken stock in USAStockData)
                {
                    SqlCommand insertQuery = new SqlCommand("INSERT INTO dbo.APIStockScrapes (StockSymbol, StockName, LastPrice, PriceChange, PercentChange) VALUES (@stockSymbol, @stockName, @lastPrice, @priceChange, @percentChange)", databaseConnection);

                    insertQuery.Parameters.AddWithValue("@stockSymbol", stock["ExchangeShortName"].ToString());
                    insertQuery.Parameters.AddWithValue("@stockName", stock["Name"].ToString());
                    insertQuery.Parameters.AddWithValue("@lastPrice", stock["Price"].ToString());
                    insertQuery.Parameters.AddWithValue("@priceChange", stock["PriceChange"].ToString());
                    insertQuery.Parameters.AddWithValue("@percentChange", stock["PercentChange"].ToString());
                    insertQuery.ExecuteNonQuery();

                }

                databaseConnection.Close();
                Console.WriteLine("Database updated");
            }

            



            // FinanceAPI.APICall();

            // Console.WriteLine(content);
            Console.ReadLine();

        }
    }
}
