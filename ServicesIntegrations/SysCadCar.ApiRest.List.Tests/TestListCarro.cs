using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysCadCar.Data.Repositories;
using SysCadCar.ApiRest.List.Controllers;
using SysCadCar.Domain.Entity;
using System.Net;
using System.Collections;
using System.Collections.Generic;

namespace SysCadCar.ApiRest.List.Tests
{
    [TestClass]
    public class TestListCarro
    {
        [TestMethod]
        public void TestMethodListCarro()
        {
            DocumentDBRepository<Carro>.Initialize();           

            CarroListController carrolist = new CarroListController();
                       
            IList<Carro> car = new List<Carro>(carrolist.ListCarro().Result);

            Assert.IsTrue(car.Count > 0);           

        }
    }
}
