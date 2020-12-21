GO

CREATE TABLE [dbo].[tb_BF_Asientos](
	[IdAsiento] [int] NOT NULL,
	[IdPromocion] [int] NULL,
	[IdDetallePromocion] [int] NOT NULL,
	[NumAsiento] [int] NOT NULL,
 CONSTRAINT [PK_tb_Asiento_Promo] PRIMARY KEY CLUSTERED 
(
	[IdAsiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
