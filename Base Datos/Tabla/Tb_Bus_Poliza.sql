GO

CREATE TABLE [dbo].[Tb_Bus_Poliza](
	[id_BusPliza] [int] IDENTITY(1,1) NOT NULL,
	[codi_Empresa] [int] NULL,
	[codi_Bus] [varchar](6) NULL,
	[Nro_Poliza] [varchar](100) NULL,
	[Fecha_Reg] [datetime] NULL,
	[Fecha_Ven] [datetime] NULL,
	[estado] [varchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_BusPliza] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO