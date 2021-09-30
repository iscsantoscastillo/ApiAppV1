USE [SoftCredito]
GO
/****** Object:  StoredProcedure [dbo].[sp_mpf_referencia_unica_get]    Script Date: 27/09/2021 01:37:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		wmorales
-- Create date: 26/08/2021
-- Description:	metodo para mostrar ventas_referencias_unicas
-- =============================================
ALTER PROCEDURE [dbo].[sp_mpf_referencia_unica_get]
	  @cve_solicitud varchar(24)
   , @plataforma    varchar(64) = null
AS
BEGIN
   SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

   DECLARE @mensaje      VARCHAR(512) 
   DECLARE @estatus      INT = -1
   DECLARE @saldo        FLOAT
   DECLARE @pago_semanal FLOAT

   IF  @plataforma = ''
      SET @plataforma = NULL

   SELECT @estatus      = estatus
        , @saldo        = pago_para_liquidar
        , @pago_semanal = pago_semanal
    FROM dbo.vw_mpf_saldos_credito saldo
   WHERE saldo.clave_solicitud = @cve_solicitud

	SELECT [cve_solicitud]
		  , [plataforma]
		  , [referencia]
        , [visible_app]
		  , [vigencia]
		  , case when monto_minimo > @saldo then @saldo else monto_minimo end [monto_minimo]
		  , @saldo as [monto_maximo]
		  , [url_imagen]
		  , [estatus]
	  FROM [dbo].[mpf_ventas_referencia_unica]
    WHERE [estatus] = 1
      AND [vigencia] >= CONVERT(DATE, GETDATE())
      AND [cve_solicitud] = @cve_solicitud
      AND [plataforma] = ISNULL(@plataforma, [plataforma])
END 

