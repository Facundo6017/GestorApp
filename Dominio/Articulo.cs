using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public long codigo { get; set; }
        public string nombre { get; set; }
        public string marca { get; set; }
        public string categoria { get; set; }
        public string urlImg { get; set; }
        public float precio { get; set; }
    }
}
