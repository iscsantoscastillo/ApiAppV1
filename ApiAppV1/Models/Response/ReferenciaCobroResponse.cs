using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAppV1.Models.Response
{
    public class ReferenciaCobroResponse
    {
        public string Referencia { get; set; }
        public int TipoDispercion { get; set; }
        public string DescripcionDispercion { get; set; }
        public string FechaLimiteCobro { get; set; }
        public decimal MontoCredito { get; set; }

    }
}
