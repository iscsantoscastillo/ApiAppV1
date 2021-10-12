using ApiTokensAppsMacropay.Models.Request;
using System;
using System.Collections.Generic;
using ApiTokensAppsMacropay.Repository;
using ApiAppV1.Models.Response;
using ApiAppV1.Models.Request;

namespace ApiTokensAppsMacropay.Service
{
    public class ProductoServiceImpl : IProductoService
    {
        private IProductoRepo _iProductoRepo;

        public ProductoServiceImpl(IProductoRepo productoRepo) {
            this._iProductoRepo = productoRepo;
        }

        public ProductoResponse ActualizarDatosClienteCash(SolicitudRequest solicitud)
        {
            return _iProductoRepo.ActualizarDatosClienteCash(solicitud);
        }

        public ProductoResponse consultar(SolicitudRequest solicitudRequest)
        {
            return _iProductoRepo.consultar(solicitudRequest);
        }
    
        public int GuardarTokenFireBase(EntradaRequest entrada)
        {
            return _iProductoRepo.GuardarTokenFireBase(entrada);
        }

        public ReferenciaCobroResponse GetReferenciaCobro(EntradaRequest entrada) {
            return _iProductoRepo.GetReferenciaCobro(entrada);
        }


    }
}
