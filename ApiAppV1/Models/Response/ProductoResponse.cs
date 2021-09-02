using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAppV1.Models.Response
{
    public class ProductoResponse
    {
        public ProductoResponse() { }
        public ProductoResponse(string refConekta, string refOpenpay, bool tieneRefConekta, bool tieneRefOpenpay) {
            this.ReferenciaConekta = refConekta;
            this.ReferenciaOpenpay = refOpenpay;
            this.TieneReferenciaConekta = tieneRefConekta;
            this.TieneReferenciaOpenpay = tieneRefOpenpay;
        }
        public string ReferenciaConekta { get; set; }
        public string ReferenciaOpenpay { get; set; }
        public bool TieneReferenciaConekta { get; set; }
        public bool TieneReferenciaOpenpay { get; set; }

    }
}
