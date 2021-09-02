using ApiAppV1.Models.Response;
using ApiTokensAppsMacropay.Helpers;
using ApiTokensAppsMacropay.Models.Request;
using ApiTokensAppsMacropay.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ApiAppV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiAppV1Controller : ControllerBase
    {
        Logger log = LogManager.GetCurrentClassLogger();
        private IProductoService _iProductoService;

        public ApiAppV1Controller(IProductoService productoService) {
            this._iProductoService = productoService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

            return new string[] { "ApiAppV1 V. " + version };

        }

        [HttpPost("GetReferencias")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetReferencias([FromBody] SolicitudRequest sol)
        {
            string mensaje = String.Empty;
            ProductoResponse productoResponse = null;
            try
            {
                productoResponse = _iProductoService.consultar(sol);

                if (productoResponse is null) {
                    return Ok(new
                    {
                        exitoso = false,
                        datos = new ProductoResponse(String.Empty, String.Empty, false, false),
                        mensaje = "No se encontró la solicitud"
                    });
                }
                
                return Ok(new
                {
                    exitoso = true,
                    datos = productoResponse,
                    mensaje = String.Empty
                });
            }
            catch (Exception ex) {
                mensaje = "Error inesperado: " + ex.Message;
                log.Info(mensaje);
                ProductoResponse p = new ProductoResponse(String.Empty, String.Empty, false, false);
                return NotFound(new
                {
                    exitoso = false,
                    datos = p,
                    mensaje = "No se pudo obtener la informacion"
                });
            }
            
        }
    }
}
