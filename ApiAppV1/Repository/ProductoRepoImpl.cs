using ApiAppV1.Models.Request;
using ApiAppV1.Models.Response;
using ApiTokensAppsMacropay.AD;
using ApiTokensAppsMacropay.Helpers;
using ApiTokensAppsMacropay.Models.Request;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ApiTokensAppsMacropay.Repository
{
    public class ProductoRepoImpl : IProductoRepo
    {
        Logger log = LogManager.GetCurrentClassLogger();
        public ProductoResponse consultar(SolicitudRequest solicitud)
        {
           
            ProductoResponse productoResponse = null;

            Conexion conexion = new Conexion();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conexion.cnCadena(Constantes.BD_SOFT)))
                {
                    cnn.Open();
                    string sp = "sp_mpf_referencia_unica_get";
                    using (SqlCommand sqlCommand = new SqlCommand(sp, cnn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@cve_solicitud", SqlDbType.VarChar);
                        sqlCommand.Parameters["@cve_solicitud"].Value = solicitud.ClaveSolicitud;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            
                            if (sqlDataReader.HasRows)
                            {
                                productoResponse = new ProductoResponse();
                                while (sqlDataReader.Read())
                                {
                                    string plataforma = sqlDataReader["plataforma"].ToString();
                                    if (plataforma.Equals("OXXO"))
                                    {
                                        productoResponse.ReferenciaConekta = sqlDataReader["referencia"].ToString();
                                        productoResponse.TieneReferenciaConekta = Int32.Parse(sqlDataReader["visible_app"].ToString()) == 1 ? true : false;
                                    }

                                    if (plataforma.Equals("OPENPAY"))
                                    {
                                        productoResponse.ReferenciaOpenpay = sqlDataReader["referencia"].ToString();
                                        productoResponse.TieneReferenciaOpenpay = Int32.Parse(sqlDataReader["visible_app"].ToString()) == 1 ? true : false;
                                    }

                                    if (plataforma.Equals("WILLYS"))
                                    {
                                        productoResponse.ReferenciaWillys = sqlDataReader["referencia"].ToString();
                                        productoResponse.TieneReferenciaWillys = Int32.Parse(sqlDataReader["visible_app"].ToString()) == 1 ? true : false;
                                    }
                                }
                            }
                            else {//No se encontró la solicitud
                                  
                            }
                        }

                    }
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return productoResponse;
        }

        public ProductoResponse ActualizarDatosClienteCash(SolicitudRequest solicitud)
        {

            ProductoResponse productoResponse = null;

            Conexion conexion = new Conexion();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conexion.cnCadena(Constantes.BD_SOFT)))
                {
                    cnn.Open();
                    string sp = "ALGUN SP";
                    using (SqlCommand sqlCommand = new SqlCommand(sp, cnn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@cve_solicitud", SqlDbType.VarChar);
                        sqlCommand.Parameters["@cve_solicitud"].Value = solicitud.ClaveSolicitud;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {

                            if (sqlDataReader.HasRows)
                            {
                                productoResponse = new ProductoResponse();
                                while (sqlDataReader.Read())
                                {
                                    string plataforma = sqlDataReader["plataforma"].ToString();
                                    if (plataforma.Equals("OXXO"))
                                    {
                                        productoResponse.ReferenciaConekta = sqlDataReader["referencia"].ToString();
                                        productoResponse.TieneReferenciaConekta = Int32.Parse(sqlDataReader["visible_app"].ToString()) == 1 ? true : false;
                                    }

                                    if (plataforma.Equals("OPENPAY"))
                                    {
                                        productoResponse.ReferenciaOpenpay = sqlDataReader["referencia"].ToString();
                                        productoResponse.TieneReferenciaOpenpay = Int32.Parse(sqlDataReader["visible_app"].ToString()) == 1 ? true : false;
                                    }

                                    if (plataforma.Equals("WILLYS"))
                                    {
                                        productoResponse.ReferenciaWillys = sqlDataReader["referencia"].ToString();
                                        productoResponse.TieneReferenciaWillys = Int32.Parse(sqlDataReader["visible_app"].ToString()) == 1 ? true : false;
                                    }
                                }
                            }
                            else
                            {//No se encontró la solicitud

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return productoResponse;
        }

        public int GuardarTokenFireBase(EntradaRequest entrada)
        {
            int resultado = 0;
            Conexion conexion = new Conexion();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conexion.cnCadena(Constantes.BD_SOFT)))
                {
                    cnn.Open();
                    string sp = "sp_mpf_ventas_token_dispositivo_add";
                    using (SqlCommand sqlCommand = new SqlCommand(sp, cnn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add("@cve_solicitud", SqlDbType.VarChar);
                        sqlCommand.Parameters["@cve_solicitud"].Value = entrada.ClaveSolicitud;

                        sqlCommand.Parameters.Add("@imei_dispositivo", SqlDbType.VarChar);
                        sqlCommand.Parameters["@imei_dispositivo"].Value = entrada.Imei;

                        sqlCommand.Parameters.Add("@token", SqlDbType.VarChar);
                        sqlCommand.Parameters["@token"].Value = entrada.TokenFirebase;
                        
                        sqlCommand.Parameters.Add("@fecha_token", SqlDbType.VarChar);
                        sqlCommand.Parameters["@fecha_token"].Value = entrada.TokenFirebase;

                        sqlCommand.Parameters.Add("@oper_alta", SqlDbType.VarChar);
                        sqlCommand.Parameters["@oper_alta"].Value = Constantes.SISTEMA;

                        sqlCommand.ExecuteNonQuery();

                        resultado = Constantes.ENTERO_UNO;

                        log.Info("Se guardó correctamente. GuardarTokenFireBase");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Ocurrió un error de base de datos. " + ex.Message);
                throw new Exception(ex.Message);
            }
            
            return resultado;
        }

        public ReferenciaCobroResponse GetReferenciaCobro(EntradaRequest solicitud)
        {

            ReferenciaCobroResponse refCobro = null;

            Conexion conexion = new Conexion();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conexion.cnCadena(Constantes.BD_SOFT)))
                {
                    cnn.Open();
                    string sp = "sp_mpe_cliente_referencia_cobro_get";
                    using (SqlCommand sqlCommand = new SqlCommand(sp, cnn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        
                        sqlCommand.Parameters.Add("@cve_solicitud", SqlDbType.VarChar);
                        sqlCommand.Parameters["@cve_solicitud"].Value = solicitud.ClaveSolicitud;

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {

                            if (sqlDataReader.HasRows)
                            {
                                refCobro = new ReferenciaCobroResponse();
                                while (sqlDataReader.Read())
                                {                                                                           
                                    //refCobro.DescripcionDispercion = sqlDataReader["referencia"].ToString();                                        
                                    refCobro.FechaLimiteCobro = sqlDataReader["fecha_vencimiento"].ToString();                                                                                                                
                                    //refCobro.MontoCredito = sqlDataReader["referencia"].ToString();                                        
                                    refCobro.Referencia = sqlDataReader["referencia"].ToString();                                                                                                              
                                    //refCobro.TipoDispercion = sqlDataReader["referencia"].ToString();
                                                                           
                                }
                            }
                            else
                            {//No se encontró la solicitud

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Ocurrío un error de base de datos: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return refCobro;
        }
    }
    
}
