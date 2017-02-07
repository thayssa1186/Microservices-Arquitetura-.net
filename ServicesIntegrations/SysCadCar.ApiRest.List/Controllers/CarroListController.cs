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

namespace SysCadCar.ApiRest.List.Controllers
{
    [RoutePrefix("api/carro")]
    public class CarroListController : ApiController
    {
        [HttpGet]
        [Route("listCarro")]
        public async Task<IEnumerable<Carro>> ListCarro()
        {

            try
            {
              
                 var result = await DocumentDBRepository<Carro>.GetAllItemsAsync();

                if (result.Count() > 0)
                {
                    return result;
                }
                else
                {
                    new Exception("Não Existe nenhum carros cadastrados");
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
