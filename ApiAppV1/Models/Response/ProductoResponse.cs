using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAppV1.Models.Response
{
    public class ProductoResponse
    {
        public ProductoResponse() {
            this.ReferenciaConekta = String.Empty;
            this.ReferenciaOpenpay = String.Empty;
            this.ReferenciaWillys = String.Empty;
            this.TieneReferenciaConekta = false;
            this.TieneReferenciaOpenpay = false;
            this.TieneReferenciaWillys = false;
        }
        public ProductoResponse(string refConekta, string refOpenpay, string refWillys, bool tieneRefConekta, bool tieneRefOpenpay, bool tieneRefWillys) {
            this.ReferenciaConekta = refConekta;
            this.ReferenciaOpenpay = refOpenpay;
            this.ReferenciaWillys = refWillys;
            this.TieneReferenciaConekta = tieneRefConekta;
            this.TieneReferenciaOpenpay = tieneRefOpenpay;
            this.TieneReferenciaWillys = tieneRefWillys;
        }
        public string ReferenciaConekta { get; set; }
        public string ReferenciaOpenpay { get; set; }
        public string ReferenciaWillys { get; set; }
        public bool TieneReferenciaConekta { get; set; }
        public bool TieneReferenciaOpenpay { get; set; }
        public bool TieneReferenciaWillys { get; set; }

    }
}
