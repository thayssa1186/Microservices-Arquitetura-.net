using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysCadCar.ApiRest.Views.Controllers;
using SysCadCar.Domain.Entity;
using SysCadCar.Data.Repositories;
using System.Collections.Generic;

namespace SysCadCar.ApiRest.Views.Tests
{
    [TestClass]
    public class TestViewCarro
    {
        [TestMethod]
        public void TestMethodViewsCarroSucesso()
        {
            string placa = "BXD1186";

            DocumentDBRepository<Carro>.Initialize();

            CarroViewsController carroView = new CarroViewsController();

            var car =   carroView.ViewsCarro(placa).Result;

            Assert.IsNotNull(car);

        }

        [TestMethod]
        public void TestMethodViewsCarroErro()
        {
            string placa = "BXD0000";

            DocumentDBRepository<Carro>.Initialize();

            CarroViewsController carroView = new CarroViewsController();

            var car = carroView.ViewsCarro(placa).Result;

            Assert.IsNull(car);

        }
    }
}
