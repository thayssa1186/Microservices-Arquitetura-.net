using SysCadCar.Mvc.App.Models;
using SysCadCar.Mvc.App.ServiceCarroWcf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysCadCar.Mvc.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Service1Client service = new Service1Client();
            var carro = service.listCarro().ToList();

            return View(carro);
        }

        // GET: Home/Details/5
        public ActionResult Details(string Placa)
        {
            
            Service1Client service = new Service1Client();
            var car = service.viewsCarro(Placa);
            var path = car.UrlFotos;
            if (Directory.Exists(Server.MapPath(path)))
            {
                ViewBag.Fotos = ProcessDirectory(path);
            }

            return View(car);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            ViewsModelCarro car = new ViewsModelCarro();
            return View(car);
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewsModelCarro model)
        {
            try
            {
                
                var caminho = string.Format("~/FotosVistoria/{0}" ,model.Placa);

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

                Service1Client service = new Service1Client();
                var car = new Carro();

                if(model.Bloqueio)
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


                if (service.AddCarro(car))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

               
            }
            catch
            {
                return View();
            }
        }
       

        private  List<string> ProcessDirectory(string targetDirectory)
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