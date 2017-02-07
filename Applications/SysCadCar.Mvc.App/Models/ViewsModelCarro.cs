using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysCadCar.Mvc.App.Models
{
    public class ViewsModelCarro
    {
        
        public string Proprietario { get; set; }

        
        public string CPF { get; set; }

       
        public string Placa { get; set; }

        
        public string Renavan { get; set; }

        
        public HttpPostedFileBase[] Fotos { get; set; }
                
        public bool Bloqueio { get; set; }
    }
}