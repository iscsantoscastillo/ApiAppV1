USE [SoftCredito]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- CREATE PROC sp_appv1_mp_referencias 
ALTER PROC sp_appv1_mp_referencias
	@CLAVE_SOLICITUD VARCHAR(50)
AS

DECLARE @TABLE TABLE (
	REF_CONEKTA VARCHAR(50),
	REF_OPENPAY VARCHAR(50),
	TIENE_REF_CONEKTA INTEGER,
	TIENE_REF_OPENPAY INTEGER
)

INSERT INTO @TABLE VALUES('CON-19183736-ZC', 'OP-94948473673627',1,1)

SELECT * FROM @TABLE;

