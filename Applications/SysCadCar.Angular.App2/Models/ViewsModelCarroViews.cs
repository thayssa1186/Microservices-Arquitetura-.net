using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysCadCar.Angular.App2.Models
{
    public class ViewsModelCarroViews
    {
        public string Proprietario { get; set; }


        public string CPF { get; set; }


        public string Placa { get; set; }


        public string Renavan { get; set; }

        public List<string> Fotos { get; set; }

        public bool Bloqueio { get; set; }
    }
}