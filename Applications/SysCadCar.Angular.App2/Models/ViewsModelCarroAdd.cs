using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysCadCar.Angular.App2.Models
{
    public class ViewsModelCarroAdd
    {
        public string Proprietario { get; set; }


        public string CPF { get; set; }


        public string Placa { get; set; }


        public string Renavan { get; set; }

        public HttpPostedFileBase[] Fotos { get; set; }

        public bool Bloqueio { get; set; }
    }
}