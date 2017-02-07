using RestSharp;
using SysCadCar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;
using SysCadCar.Domain.Enun;

namespace UtilHttpRequestResponse
{
    public class GetRequest<T> where T : class
    {
        public IRestResponse getJson(string urlbase, string acao)
        {

            var client = new RestClient(urlbase);

            var request = new RestRequest(acao, Method.GET);
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse getJson(string urlbase, string acao, Dictionary<string, string> parametros, Param tipo)
        {

            var client = new RestClient(urlbase);

            var request = new RestRequest(acao, Method.GET);
            request.RequestFormat = DataFormat.Json;

            if (tipo == Param.Parameter)
            {
                foreach (KeyValuePair<string, string> kvp in parametros)
                {
                    request.AddParameter(kvp.Key, kvp.Value);
                   
                }
            }
            else if(tipo == Param.UrlSegment)
            {
                foreach (KeyValuePair<string, string> kvp in parametros)
                {
                    request.AddUrlSegment(kvp.Key, kvp.Value);

                }

            }
            else if (tipo == Param.QueryString)
            {
                foreach (KeyValuePair<string, string> kvp in parametros)
                {
                    request.AddQueryParameter(kvp.Key, kvp.Value);

                }

            }

            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
