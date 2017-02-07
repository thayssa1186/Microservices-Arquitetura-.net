using RestSharp;
using SysCadCar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UtilHttpRequestResponse
{
    public class PostRequest<T> where T : class
    {
        public IRestResponse postJson(string urlbase, string acao,T Object)
        {
            
            var client = new RestClient(urlbase);

            var request = new RestRequest(acao, Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(Object);

            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
