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

            return new string[] { "UsuariosApi V. " + version };

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
                ProductoResponse p = new ProductoResponse(String.Empty, String.Empty);
                return NotFound(new
                {
                    exitoso = false,
                    datos = p,
                    mensaje = "Error inesperado"                   
                });
            }
            
        }
    }
}
