GO

CREATE TABLE [dbo].[tb_BF_Det_Promo](
	[IdDetallePromocion] [int] IDENTITY(1,1) NOT NULL,
	[IdPromocion] [int] NOT NULL,
	[Codi_Origen] [smallint] NOT NULL,
	[Codi_PuntoVenta] [smallint] NOT NULL,
	[Codi_Destino] [smallint] NOT NULL,
	[Codi_Servicio] [tinyint] NOT NULL,
	[Hora] [varchar](8) NOT NULL,
	[Codi_Asiento] [int] NOT NULL,
	[Tipo] [varchar](2) NOT NULL,
	[Monto] [decimal](18, 2) NOT NULL,
	[Codi_Usuario] [smallint] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_tb_Det_Promo] PRIMARY KEY CLUSTERED 
(
	[IdDetallePromocion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO