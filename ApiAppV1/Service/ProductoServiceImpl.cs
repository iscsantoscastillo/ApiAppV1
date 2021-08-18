using ApiTokensAppsMacropay.Models.Request;
using System;
using System.Collections.Generic;
using ApiTokensAppsMacropay.Repository;
using ApiAppV1.Models.Response;

namespace ApiTokensAppsMacropay.Service
{
    public class ProductoServiceImpl : IProductoService
    {
        private IProductoRepo _iProductoRepo;

        public ProductoServiceImpl(IProductoRepo productoRepo) {
            this._iProductoRepo = productoRepo;
        }

        public ProductoResponse consultar(SolicitudRequest solicitudRequest)
        {
            return _iProductoRepo.consultar(solicitudRequest);
        }

       
    }
}
