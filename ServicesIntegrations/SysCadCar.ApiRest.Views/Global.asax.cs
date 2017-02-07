using SysCadCar.Data.Repositories;
using SysCadCar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace SysCadCar.ApiRest.Views
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            DocumentDBRepository<Carro>.Initialize();
        }
    }
}
