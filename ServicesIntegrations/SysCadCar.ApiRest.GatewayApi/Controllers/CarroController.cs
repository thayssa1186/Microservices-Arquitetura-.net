using Newtonsoft.Json.Linq;
using SysCadCar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UtilHttpRequestResponse;
using Newtonsoft.Json;
using RestSharp;
using System.Collections;
using SysCadCar.Domain.Enun;

namespace SysCadCar.ApiRest.GatewayApi.Controllers
{
    [RoutePrefix("api/v1/carro")]
    public class CarroController : ApiController
    {
        [Route("addCarro")]
        [HttpPost]
        public HttpResponseMessage AddCarro(JObject request)
        {
            var url = ConfigurationManager.AppSettings["endpointCarroAdd"];
            var action = ConfigurationManager.AppSettings["actionCarrodd"];

            this.Request = new HttpRequestMessage();

            var carroAdd = request.ToObject<Carro>();

            PostRequest<Carro> post = new PostRequest<Carro>();
            var result = post.postJson(url, action, carroAdd);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created);
                return response;
            }
            else
            {
                HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Erro ao adicionar Veiculo");
                return response;
            }
        }

        [Route("listCarro")]
        [HttpGet]
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

        [Route("viewsCarro/{placa}")]
        [HttpGet]
        public Carro viewsCarro(string placa)
        {
            var url = ConfigurationManager.AppSettings["endpointCarroViews"];
            var action = ConfigurationManager.AppSettings["actionCarroViews"];



            GetRequest<Carro> get = new GetRequest<Carro>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("placa", placa);

            var result = get.getJson(url, action,dict,Param.UrlSegment);

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
