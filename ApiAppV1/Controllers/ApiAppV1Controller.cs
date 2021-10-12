using ApiAppV1.Models.Request;
using ApiAppV1.Models.Response;
using ApiTokensAppsMacropay.Helpers;
using ApiTokensAppsMacropay.Models.Request;
using ApiTokensAppsMacropay.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //Auth Bearer
        [Authorize]//Basic Auth
        public async Task<IActionResult> GetReferencias([FromBody] SolicitudRequest sol)
        {
            //@@EL SP YA ESTA LISTO, PERO SI SE CORRE Y SE MODIFICA, AFECTARA LO QUE HAY EN LA PUBLOCACION
            string mensaje = String.Empty;
            ProductoResponse productoResponse = null;
            try
            {
                productoResponse = _iProductoService.consultar(sol);

                if (productoResponse is null) {
                    return Ok(new
                    {
                        exitoso = false,
                        datos = new ProductoResponse(String.Empty, String.Empty, String.Empty, false, false, false),
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
                ProductoResponse p = new ProductoResponse(String.Empty, String.Empty, String.Empty, false, false, false);
                return NotFound(new
                {
                    exitoso = false,
                    datos = p,
                    mensaje = "No se pudo obtener la informacion"
                });
            }
            
        }

        [HttpPost("ActualizaDatosClienteCash")]
        [ApiExplorerSettings(IgnoreApi = true)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //Auth Bearer
        [Authorize]//Basic Auth
        public async Task<IActionResult> ActualizaDatosClienteCash([FromBody] SolicitudRequest sol)
        {
            string mensaje = null;
            try
            {               
                return Ok(new
                {
                    exitoso = true,                    
                    mensaje = String.Empty
                });
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado: " + ex.Message;
                log.Info(mensaje);
                return NotFound(new
                {
                    exitoso = false,                    
                    mensaje = "Ocurrió un problema que impidió la actualización de datos"
                });
            }

        }


        [HttpPost("CrearTokenFirebase")]
        [ApiExplorerSettings(IgnoreApi = true)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //Auth Bearer
        [Authorize]//Basic Auth
        public async Task<IActionResult> CrearTokenFirebase([FromBody] JsonElement ent )
        {
            string mensaje = null;

            try
            {
                //EntradaRequest
                var json = ent.GetRawText();
                //Grabar en bitácora el Json enviado
                //this._iPagoService.GrabarBitacora(json);
                log.Info("JSON del método CrearTokenFirebase: " + json);
                EntradaRequest entrada = JsonConvert.DeserializeObject<EntradaRequest>(json);
            
                int resultado = _iProductoService.GuardarTokenFireBase(entrada);

                if (resultado == Constantes.ENTERO_UNO)
                {
                    return Ok(new
                    {
                        exitoso = true,
                        mensaje = String.Empty
                    });
                }
                else {
                    return Ok(new
                    {
                        exitoso = false,
                        mensaje = "Ocurrió un problema que impidió guardar los datos"
                    });
                }                                      
            }catch (Exception ex)
            {
                mensaje = "Error inesperado: " + ex.Message;
                log.Info(mensaje);
                return Ok(new
                {
                    exitoso = false,
                    mensaje = "Ocurrió un problema que impidió guardar los datos"
                });
            }

        }


        [HttpPost("GetReferenciaCobro")]
        [ApiExplorerSettings(IgnoreApi = true)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //Auth Bearer
        [Authorize]//Basic Auth
        public async Task<IActionResult> GetReferenciaCobro([FromBody] JsonElement ent)
        {
            string mensaje = null;

            try
            {
                //EntradaRequest
                var json = ent.GetRawText();
                //Grabar en bitácora el Json enviado
                //this._iPagoService.GrabarBitacora(json);
                log.Info("JSON del método GetReferenciaCobro: " + json);
                EntradaRequest entrada = JsonConvert.DeserializeObject<EntradaRequest>(json);

                ReferenciaCobroResponse resultado = _iProductoService.GetReferenciaCobro(entrada);

                if (resultado is null)
                {
                    return Ok(new
                    {
                        exitoso = false,
                        mensaje = "Ocurrió un problema que impidió obtener los datos"
                    });
                    
                }
                else
                {
                    return Ok(new
                    {
                        exitoso = true,
                        mensaje = String.Empty
                    });
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado: " + ex.Message;
                log.Info(mensaje);
                return Ok(new
                {
                    exitoso = false,
                    mensaje = "Ocurrió un problema que impidió guardar los datos"
                });
            }

        }

    }
}
