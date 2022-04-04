using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaCodigoNet.Models
{
    public class Universidades
    {
        public string Country { get; set; }
        public List<string> Domains { get; set; }
        public string AlphaTwoCode { get; set; }
        public List<string> web_pages { get; set; }
        public string Imagen { get; set; }
        public string Name { get; set; }
        public bool favorito { get; set; }
    }
}
