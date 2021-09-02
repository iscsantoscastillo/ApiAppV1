using ApiAppV1.Models.Response;
using ApiTokensAppsMacropay.AD;
using ApiTokensAppsMacropay.Helpers;
using ApiTokensAppsMacropay.Models.Request;


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ApiTokensAppsMacropay.Repository
{
    public class ProductoRepoImpl : IProductoRepo
    {
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

    
    }
}
