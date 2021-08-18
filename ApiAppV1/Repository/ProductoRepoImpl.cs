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
                    string sp = "sp_appv1_mp_referencias";
                    using (SqlCommand sqlCommand = new SqlCommand(sp, cnn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@CLAVE_SOLICITUD", SqlDbType.VarChar);
                        sqlCommand.Parameters["@CLAVE_SOLICITUD"].Value = solicitud.ClaveSolicitud;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.HasRows)
                            {
                                
                                productoResponse = new ProductoResponse();
                                while (sqlDataReader.Read())
                                {
                                    productoResponse.ReferenciaConekta = sqlDataReader["REF_CONEKTA"].ToString();
                                    productoResponse.ReferenciaOpenpay = sqlDataReader["REF_OPENPAY"].ToString();
                                    

                                   
                                }
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
