using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SysCadCar.Data.Repositories;
using SysCadCar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SysCadCar.ApiRest.Add.Controllers
{
    [RoutePrefix("api/carro")]
    public class CarroAddController : ApiController
    {
        // POST api/carro/addCarro
        [HttpPost]
        [Route("addCarro")]
        public async Task<HttpResponseMessage> AddCarro(JObject request)
        {
            this.Request = new HttpRequestMessage();
                       
            try
            {               

                if (ModelState.IsValid)
                {
                    Carro carro = request.ToObject<Carro>();

                    if (carro != null)
                    {
                        if (!string.IsNullOrEmpty(carro.CPF))
                        {
                            var result = await DocumentDBRepository<Carro>.GetItemsAsync(p => p.Placa.Equals(carro.Placa));

                            if (result.Count() == 0)
                            {
                                await DocumentDBRepository<Carro>.CreateItemAsync(carro);

                                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created);

                                return response;
                            }
                            else
                            {
                                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Placa já existente");
                            }
                        }
                        else
                        {
                            return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CPF é inválido");
                        }
                    }
                    else
                    {
                        return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Obejeto nulo");
                    }

                }
                else
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
