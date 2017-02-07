using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysCadCar.Domain.Entity;
using SysCadCar.ApiRest.GatewayApi.Controllers;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Collections.Generic;
using SysCadCar.Domain.Enun;

namespace SysCadCar.ApiRest.GatewayApi.Tests
{
    [TestClass]
    public class TestGatewayApi
    {
        [TestMethod]
        public void TestMethodAddCarroExistente()
        {          

            var carro = new Carro { CPF = "33508848817", Placa = "BXD1186", Proprietario = "Thaissa Bueno Sanches", Renavan = "123456777877777", UrlFotos = "http://galeria/33508848817/BXZ2345/010220172909/", Bloqueio = Status.NaoBloqueado };

            CarroController carroAdd = new CarroController();

            var result = carroAdd.AddCarro(JObject.FromObject(carro));

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);

        }

        [TestMethod]
        public void TestMethodAddCarroNaoExistente()
        {
            var carro = new Carro { CPF = "33508848817", Placa = "BXZ9999", Proprietario = "Thaissa Bueno Sanches", Renavan = "123456777877777", UrlFotos = "http://galeria/33508848817/BXZ2345/010220172909/", Bloqueio = Status.NaoBloqueado };

            CarroController carroAdd = new CarroController();

            var result = carroAdd.AddCarro(JObject.FromObject(carro));

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

        }

        [TestMethod]
        public void TestMethodListCarro()
        {
            CarroController carrolist = new CarroController();

            IList<Carro> car = new List<Carro>(carrolist.listCarro());

            Assert.IsTrue(car.Count > 0);

        }

        [TestMethod]
        public void TestMethodViewsCarroSucesso()
        {
            string placa = "BXZ9999";

            CarroController carroView = new CarroController();

            var car = carroView.viewsCarro(placa);

            Assert.IsNotNull(car);

        }

        [TestMethod]
        public void TestMethodViewsCarroErro()
        {
            string placa = "BXD0000";

            CarroController carroView = new CarroController();

            var car = carroView.viewsCarro(placa);

            Assert.IsNull(car);

        }
    }
}
