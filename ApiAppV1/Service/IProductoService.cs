using ApiAppV1.Models.Request;
using ApiAppV1.Models.Response;
using ApiTokensAppsMacropay.Models.Request;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTokensAppsMacropay.Service
{
    public interface IProductoService
    {
        public ProductoResponse consultar(SolicitudRequest solicitudRequest);
        public ProductoResponse ActualizarDatosClienteCash(SolicitudRequest solicitud);
        public int GuardarTokenFireBase(EntradaRequest entrada);
        public ReferenciaCobroResponse GetReferenciaCobro(EntradaRequest entrada);

    }
}
