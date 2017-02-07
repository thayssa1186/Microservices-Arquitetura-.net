using Newtonsoft.Json;
using SysCadCar.Angular.App2.Models;
using SysCadCar.Domain.Entity;
using SysCadCar.Domain.Enun;
using SysCadCar.Mvc.App.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilHttpRequestResponse;

namespace SysCadCar.Mvc.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           

            return View();
        }

        public JsonResult GetAllCars()
        {

            var url = ConfigurationManager.AppSettings["endpointCarroGateway"];
            var action = ConfigurationManager.AppSettings["actionCarroList"];



            GetRequest<Carro> get = new GetRequest<Carro>();
            var result = get.getJson(url, action).Content;

            var resultresp = JsonConvert.DeserializeObject<IEnumerable<Carro>>(result);


            return Json(resultresp, JsonRequestBehavior.AllowGet);
          
        }

        // GET: Home/Details/5
        public JsonResult Details(string Placa)
        {

            var url = ConfigurationManager.AppSettings["endpointCarroViews"];
            var action = ConfigurationManager.AppSettings["actionCarroViews"];



            GetRequest<Carro> get = new GetRequest<Carro>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("placa", Placa);

            var result = get.getJson(url, action, dict, Param.UrlSegment);           
            var resultresp = JsonConvert.DeserializeObject<Carro>(result.Content);

            ViewsModelCarroViews car = new ViewsModelCarroViews();
            car.Bloqueio = resultresp.Bloqueio == Status.Bloqueado ? true : false;
            car.CPF = resultresp.CPF;

            var path = resultresp.UrlFotos;
            if (Directory.Exists(Server.MapPath(path)))
            {
                car.Fotos = ProcessDirectory(path);
            }

            car.Placa = resultresp.Placa;
            car.Proprietario = resultresp.Proprietario;
            car.Renavan = resultresp.Renavan;

            return Json(car, JsonRequestBehavior.AllowGet);
        }

       

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Create(ViewsModelCarro model)
        {
            var url = ConfigurationManager.AppSettings["endpointCarroAdd"];
            var action = ConfigurationManager.AppSettings["actionCarrodd"];

            try
            {

                var caminho = string.Format("~/FotosVistoria/{0}", model.Placa);

                if (Directory.Exists(Server.MapPath(caminho)))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(caminho));
                }

                var validImageTypes = new string[]
               {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
               };

                foreach (HttpPostedFileBase file in model.Fotos)
                {

                    if (file == null || file.ContentLength == 0)
                    {
                        ModelState.AddModelError("Fotos", "Campo requerido");
                    }
                    else if (!validImageTypes.Contains(file.ContentType))
                    {
                        ModelState.AddModelError("Fotos", "Tipo de imagem iválido , permitido apenas gif,jpg e png");
                    }

                    if (ModelState.IsValid)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var uploadDir = caminho;
                            var imagePath = Path.Combine(Server.MapPath(uploadDir), file.FileName);
                            file.SaveAs(imagePath);
                        }
                    }
                }

               
                var car = new Carro();

                if (model.Bloqueio)
                {
                    car.Bloqueio = Status.Bloqueado;
                }
                else
                {
                    car.Bloqueio = Status.NaoBloqueado;
                }

                car.CPF = model.CPF;
                car.Placa = model.Placa;
                car.Proprietario = model.Proprietario;
                car.Renavan = model.Renavan;
                car.UrlFotos = caminho;

               

                PostRequest<Carro> post = new PostRequest<Carro>();
                var result = post.postJson(url, action, car);

                return "Carro adicionado com sucesso";




            }
            catch
            {
                return "Erro ao Adicionar Carro";
            }
        }


        private List<string> ProcessDirectory(string targetDirectory)
        {
            List<string> fileList = new List<string>();

            string[] fileEntries = Directory.GetFiles(Server.MapPath(targetDirectory));
            foreach (string fileName in fileEntries)
            {

                var directory = targetDirectory.Replace("~/", "../");
                var nameFile = Path.GetFileName(fileName);

                var imageUrl = string.Format("{0}/{1}", directory, nameFile);

                fileList.Add(imageUrl);
            }

            return fileList;

        }


    }
}