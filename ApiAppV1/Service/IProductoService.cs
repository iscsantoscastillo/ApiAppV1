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
        ProductoResponse consultar(SolicitudRequest solicitudRequest);
       
    }
}
