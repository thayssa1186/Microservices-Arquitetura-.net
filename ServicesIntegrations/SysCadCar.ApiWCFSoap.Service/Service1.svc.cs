using Newtonsoft.Json;
using SysCadCar.Domain.Entity;
using SysCadCar.Domain.Enun;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UtilHttpRequestResponse;

namespace SysCadCar.ApiWCFSoap.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
       
        public bool AddCarro(Carro carroAdd)
        {
            var url = ConfigurationManager.AppSettings["endpointCarroAdd"];
            var action = ConfigurationManager.AppSettings["actionCarrodd"];

           

            PostRequest<Carro> post = new PostRequest<Carro>();
            var result = post.postJson(url, action, carroAdd);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                
                return true;
            }
            else
            {
               
                return false;
            }
        }

      
        public IEnumerable<Carro> listCarro()
        {
            var url = ConfigurationManager.AppSettings["endpointCarroList"];
            var action = ConfigurationManager.AppSettings["actionCarroList"];



            GetRequest<Carro> get = new GetRequest<Carro>();
            var result = get.getJson(url, action);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultresp = JsonConvert.DeserializeObject<IEnumerable<Carro>>(result.Content);
                return resultresp;
            }
            else
            {
                return null;
            }

        }

       
        public Carro viewsCarro(string placa)
        {
            var url = ConfigurationManager.AppSettings["endpointCarroViews"];
            var action = ConfigurationManager.AppSettings["actionCarroViews"];



            GetRequest<Carro> get = new GetRequest<Carro>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("placa", placa);

            var result = get.getJson(url, action, dict, Param.UrlSegment);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultresp = JsonConvert.DeserializeObject<Carro>(result.Content);
                return resultresp;
            }
            else
            {
                return null;
            }
        }

    }
}
