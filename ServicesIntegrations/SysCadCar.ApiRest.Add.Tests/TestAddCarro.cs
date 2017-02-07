using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilHttpRequestResponse;
using SysCadCar.Domain.Entity;
using SysCadCar.Domain.Enun;
using RestSharp;
using SysCadCar.ApiRest.Add.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using SysCadCar.Data.Repositories;

namespace SysCadCar.ApiRest.Add.Tests
{
    [TestClass]
    public class TestAddCarro
    {
        [TestMethod]
        public  void TestMethodAddCarroExistente()
        {
            DocumentDBRepository<Carro>.Initialize();

            var carro = new Carro { CPF = "33508848817", Placa = "BXZ2345", Proprietario = "Thaissa Bueno Sanches", Renavan = "123456777877777", UrlFotos = "http://galeria/33508848817/BXZ2345/010220172909/", Bloqueio = Status.NaoBloqueado };

            CarroAddController carroadd = new CarroAddController();

            var result =  carroadd.AddCarro(JObject.FromObject(carro));
            
            Assert.AreEqual(HttpStatusCode.BadRequest, result.Result.StatusCode);

        }

        [TestMethod]
        public void TestMethodAddCarroNaoExistente()
        {
            DocumentDBRepository<Carro>.Initialize();

            var carro = new Carro { CPF = "33508848817", Placa = "BXZ9648", Proprietario = "Thaissa Bueno Sanches", Renavan = "123456777877777", UrlFotos = "http://galeria/33508848817/BXZ2345/010220172909/", Bloqueio = Status.NaoBloqueado };

            CarroAddController carroadd = new CarroAddController();

            var result = carroadd.AddCarro(JObject.FromObject(carro));

            Assert.AreEqual(HttpStatusCode.Created, result.Result.StatusCode);

        }
    }
}
