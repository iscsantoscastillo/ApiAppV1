using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAppV1.Models.Request
{
    public class EntradaRequest
    {
        public string TokenFirebase { get; set; }
        public string Imei { get; set; }
        public string ClaveSolicitud { get; set; }
    }
}
