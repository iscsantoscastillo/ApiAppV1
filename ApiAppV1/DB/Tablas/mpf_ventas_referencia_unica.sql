USE [SoftCredito]
GO

/****** Object:  Table [dbo].[mpf_ventas_referencia_unica]    Script Date: 27/09/2021 01:47:27 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[mpf_ventas_referencia_unica](
	[cve_solicitud] [varchar](24) NOT NULL,
	[plataforma] [varchar](16) NOT NULL,
	[vigencia] [date] NOT NULL,
	[referencia] [varchar](64) NOT NULL,
	[visible_app] [int] NOT NULL,
	[monto_minimo] [float] NULL,
	[monto_maximo] [float] NULL,
	[url_imagen] [varchar](2048) NULL,
	[estatus] [int] NOT NULL,
	[oper_alta] [varchar](16) NOT NULL,
	[fecha_alta] [datetime] NOT NULL,
	[oper_baja] [varchar](16) NULL,
	[fecha_baja] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[mpf_ventas_referencia_unica] ADD  DEFAULT ((1)) FOR [visible_app]
GO

ALTER TABLE [dbo].[mpf_ventas_referencia_unica] ADD  DEFAULT ((0)) FOR [monto_minimo]
GO

ALTER TABLE [dbo].[mpf_ventas_referencia_unica] ADD  DEFAULT ((0)) FOR [monto_maximo]
GO

ALTER TABLE [dbo].[mpf_ventas_referencia_unica] ADD  DEFAULT ((1)) FOR [estatus]
GO

ALTER TABLE [dbo].[mpf_ventas_referencia_unica] ADD  DEFAULT ('sys') FOR [oper_alta]
GO

ALTER TABLE [dbo].[mpf_ventas_referencia_unica] ADD  DEFAULT (getdate()) FOR [fecha_alta]
GO


