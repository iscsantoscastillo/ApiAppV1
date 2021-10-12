USE [SoftCredito]
GO
/****** Object:  StoredProcedure [dbo].[sp_mpf_ventas_token_dispositivo_add]    Script Date: 08/10/2021 01:14:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		wmorales	
-- Create date: 08/10/2021
-- Description:	procemdimiento para almacenar el token 
-- =============================================
ALTER PROCEDURE [dbo].[sp_mpf_ventas_token_dispositivo_add] 
	-- Add the parameters for the stored procedure here
		@cve_solicitud varchar (20)
		,@imei_dispositivo varchar (20)
		,@token varchar(max)
		,@fecha_token varchar(50)
		,@oper_alta varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	 IF NOT EXISTS(select cve_solicitud  from mpf_ventas_token_dispositivo 
	 where cve_solicitud=@cve_solicitud)
	 begin
    -- Insert statements for procedure here
	INSERT INTO [dbo].[mpf_ventas_token_dispositivo]
           ([cve_solicitud]
           ,[imei_dispositivo]
           ,[token]
           ,[fecha_token]
           ,[oper_alta]
           ,[fecha_alta]
           ,[estatus])
     VALUES
           (@cve_solicitud
           ,@imei_dispositivo
           ,@token
           ,@fecha_token
           ,@oper_alta
           ,getdate()
           ,1)
	end
END
