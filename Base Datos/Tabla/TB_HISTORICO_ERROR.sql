GO

CREATE TABLE [dbo].[TB_HISTORICO_ERROR](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Modulo] [varchar](1) NULL,
	[tipo] [varchar](1) NULL
) ON [PRIMARY] 
SET ANSI_PADDING ON
ALTER TABLE [dbo].[TB_HISTORICO_ERROR] ADD [serie] [varchar](16) NULL
ALTER TABLE [dbo].[TB_HISTORICO_ERROR] ADD [numero] [int] NULL
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[TB_HISTORICO_ERROR] ADD [errNumber] [varchar](1500) NULL
ALTER TABLE [dbo].[TB_HISTORICO_ERROR] ADD [errSeverity] [varchar](1500) NULL
ALTER TABLE [dbo].[TB_HISTORICO_ERROR] ADD [errState] [varchar](1500) NULL
ALTER TABLE [dbo].[TB_HISTORICO_ERROR] ADD [errProcedure] [varchar](1500) NULL
ALTER TABLE [dbo].[TB_HISTORICO_ERROR] ADD [errLine] [varchar](1500) NULL
ALTER TABLE [dbo].[TB_HISTORICO_ERROR] ADD [errMessage] [varchar](1500) NULL
SET ANSI_PADDING ON
ALTER TABLE [dbo].[TB_HISTORICO_ERROR] ADD [CODI_DOCUMENTO] [varchar](2) NULL
ALTER TABLE [dbo].[TB_HISTORICO_ERROR] ADD [POSICION] [int] NULL
