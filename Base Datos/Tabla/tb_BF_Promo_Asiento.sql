GO

CREATE TABLE [dbo].[tb_BF_Promo_Asiento](
	[IdPromocion] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](6) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[Codi_Empresa] [tinyint] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[FechaInicio] [datetime] NOT NULL,
	[FechaFin] [datetime] NOT NULL,
	[Codi_Usuario] [smallint] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_tb_Promo_Asiento_1] PRIMARY KEY CLUSTERED 
(
	[IdPromocion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO