﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestSharp;
using System.Data.SqlClient;
// using System.Web.Script;


namespace FinanceScrape_RestSharp
{
    class FinanceAPI
    {
        public static void APICall()
        {
            RestClient client = new RestClient("https://morning-star.p.rapidapi.com");

            RestRequest request = new RestRequest("/market/get-summary", Method.GET);
            request.AddHeader("x-rapidapi-host", "morning-star.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "fb96429169msh172f9e8369dbb6dp11b294jsn617d9bce3a4d");

            IRestResponse response = client.Execute(request);
            var content = response.Content;           
        }

    }
}
