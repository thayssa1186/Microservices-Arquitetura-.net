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


namespace SysCadCar.ApiRest.Views.Controllers
{
    [RoutePrefix("api/carro")]
    public class CarroViewsController : ApiController
    {
        [HttpGet]
        [Route("viewsCarro/{placa}")]
        public async Task<Carro> ViewsCarro(string placa)
        {

            try
            {

                var result = await DocumentDBRepository<Carro>.GetItemsAsync(p=>p.Placa.Equals(placa));

                if (result.Count() > 0)
                {
                    return result.FirstOrDefault<Carro>();
                }
                else
                {
                    new Exception("Não Existe nenhum carro cadastrado com essa placa");
                    return null;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
