using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTokensAppsMacropay.Models.Request
{
    public class SolicitudRequest
    {
        public string ClaveSolicitud { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Firma { get; set; }
    }
}
