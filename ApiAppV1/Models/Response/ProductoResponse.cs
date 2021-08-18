using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAppV1.Models.Response
{
    public class ProductoResponse
    {
        public ProductoResponse() { }
        public ProductoResponse(string refConekta, string refOpenpay) {
            this.ReferenciaConekta = refConekta;
            this.ReferenciaOpenpay = refOpenpay;
        }
        public string ReferenciaConekta { get; set; }
        public string ReferenciaOpenpay { get; set; }

    }
}
