--------------------VALIDA EXISTENCIA DE TABLA-------------------------
---Tabla Tb_Tiempo_AnticipadoCompra
Go
If Exists(Select Top 1 1 From INFORMATION_SCHEMA.TABLES Where TABLE_SCHEMA='dbo' and TABLE_NAME='Tb_Tiempo_AnticipadoCompra')
	Begin
		Drop Table Tb_Tiempo_AnticipadoCompra
	End
Go

Go
Create Table Tb_Tiempo_AnticipadoCompra(
	Cod_Origen		smallint,
	Cod_Destino		smallint,
	Minutos			smallint
)
Go

Go
Insert Into Tb_Tiempo_AnticipadoCompra(
	Cod_Origen		, Cod_Destino		, Minutos
)
Values(
	0				, 0					, 180
)
Go


---Tabla Tb_Info_TransaccionVisaNet
Go
If Exists(Select Top 1 1 From INFORMATION_SCHEMA.TABLES Where TABLE_SCHEMA='dbo' and TABLE_NAME='Tb_Info_TransaccionVisaNet')
	Begin
		Drop Table Tb_Info_TransaccionVisaNet
	End
Go

Go
Create Table Tb_Info_TransaccionVisaNet
(
Id_WebOrders		int,
Nro_Orden			Int,
Importe_Autorizado	Real,
NumTarjetaHabiente	Varchar(200),
NomTarjeta			Varchar(100),
Transaccion_Id		Varchar(20),
Fecha_Compra		SmallDateTime,
Estado_Transaccion	Varchar(50),
Cod_Accion			Varchar(5),
Mensaje_Error		Varchar(250),
Trans_Fraude		Bit
)
Go

---Tabla Tb_IgvHistorio
Go
If Not Exists(Select Top 1 1 From INFORMATION_SCHEMA.TABLES Where TABLE_SCHEMA='dbo' and TABLE_NAME='Tb_IgvHistorio')
	Begin
		CREATE TABLE [dbo].[Tb_IgvHistorio](
			[fecha] [datetime] NULL,
			[igv] [numeric](18, 0) NULL
		) ON [PRIMARY]
	End
Go

---Tabla TB_HISTORICO_ERROR
Go
If Not Exists(Select Top 1 1 From INFORMATION_SCHEMA.TABLES Where TABLE_SCHEMA='dbo' and TABLE_NAME='TB_HISTORICO_ERROR')
	Begin
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
	End
Go


---Tabla Tb_Bus_Poliza
Go
If Not Exists(Select Top 1 1 From INFORMATION_SCHEMA.TABLES Where TABLE_SCHEMA='dbo' and TABLE_NAME='Tb_Bus_Poliza')
	Begin
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
	End
Go

---Tabla Tb_BoletoRuta
Go
If Not Exists(Select Top 1 1 From INFORMATION_SCHEMA.TABLES Where TABLE_SCHEMA='dbo' and TABLE_NAME='Tb_BoletoRuta')
	Begin
		Create Table Tb_BoletoRuta(
		BoletoRutaId		BigInt	Primary Key Identity,
		Codi_Programacion	Int,
		Codi_Empresa		Tinyint,
		Fecha_Venta			Smalldatetime,
		Fecha_Liquidacion	Smalldatetime,
		Fecha_Viaje			Smalldatetime,
		Codi_Boletero		Varchar(10),
		Codi_Origen			SmallInt,--Origen Pasajero
		Codi_Destino		SmallInt,--Destino Pasajero
		Codi_Sucursal		Smallint,
		Codi_PuntoVenta		Smallint,
		TipoDoc				SmallInt,
		Serie				Varchar(6),
		Numero				BigInt,
		Exonerado			Real,
		ValorVenta			Real,
		IGV					Real,
		Total				Real,
		Anulado				Char(1),
		Fecha_Anulacion		Smalldatetime,
		EstadoBoleto		Char(1),
		TD_Pasajero			Tinyint,
		ND_Pasajero			Varchar(15),
		Nom_Pasajero		Varchar(100),
		AP_Pasajero			Varchar(100),
		AM_Pasajero			Varchar(100),
		Sexo				Char(1),
		Ruc_Pasajero		Varchar(11),
		Nro_Asiento			SmallInt,
		Imp_Manifiesto		Varchar(4),
		Control				Tinyint,
		Tipo				Char(1)
		)

	End
Go


---Tabla Tb_BF_Promo_Asiento
Go
If Not Exists(Select Top 1 1 From INFORMATION_SCHEMA.TABLES Where TABLE_SCHEMA='dbo' and TABLE_NAME='Tb_BF_Promo_Asiento')
	Begin
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
	End
Go

---Tabla Tb_BF_Det_Promo
Go
If Not Exists(Select Top 1 1 From INFORMATION_SCHEMA.TABLES Where TABLE_SCHEMA='dbo' and TABLE_NAME='Tb_BF_Det_Promo')
	Begin
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
	End
Go

---Tabla Tb_BF_Asientos
Go
If Not Exists(Select Top 1 1 From INFORMATION_SCHEMA.TABLES Where TABLE_SCHEMA='dbo' and TABLE_NAME='Tb_BF_Asientos')
	Begin
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
	End
Go

---Tabla Tb_Auditoria_Venta
Go
If Not Exists(Select Top 1 1 From INFORMATION_SCHEMA.TABLES Where TABLE_SCHEMA='dbo' and TABLE_NAME='Tb_Auditoria_Venta')
	Begin
		Create Table Tb_Auditoria_Venta(
		Id_venta			Int,
		Id_VentaWeb			Int,
		Serie_Boleto		SmallInt,
		Numero_Boleto		Int,
		Tipo				Char(1),
		Codi_Empresa		Tinyint,
		FechaViaje			Varchar(10),
		FechaVenta			Varchar(10),
		FechaRegistro		SmallDatetime,
		HoraProgamacion		Varchar(10),
		HoraViaje			Varchar(10),
		HoraEmbarque		Varchar(10),
		Codi_programacion	Int,
		Codi_Usuario		SmallInt,
		Codi_Origen			SmallInt,
		Codi_Destino		SmallInt,
		Codi_Servicio		Tinyint,
		Codi_Embarque		SmallInt,
		Codi_Arribo			SmallInt,
		Precio				Real,
		Pasajero			Varchar(200),
		Dni					Varchar(20),
		Ruc					Varchar(11),
		Asiento				Tinyint,
		Indi_Anulado		VarChar(3),
		Fecha_Anulacion		SmallDatetime,
		Codi_Usuario_Anu	SmallInt,
		Flag_Venta			Varchar(3),
		Codi_Sucursal		SmallInt,
		Codi_PuntoVenta		SmallInt,
		TipoMovimiento		Varchar(200)
		)
	End
Go

--------------VALIDAR COLUMNAS WEBORDERS------------------
GO
	IF NOT EXISTS(Select Top 1 1 From INFORMATION_SCHEMA.COLUMNS Where COLUMN_NAME='Email_Cliente' and TABLE_NAME='WebOrders')	
		BEGIN
			Alter Table WebOrders
			Add Email_Cliente	Varchar(200)
		END
GO
--------------VALIDAR COLUMNAS VENTA WEB------------------
GO
	IF NOT EXISTS(Select Top 1 1  From INFORMATION_SCHEMA.COLUMNS Where COLUMN_NAME='Indi_Anulado' and TABLE_NAME='VentaWeb')	
		BEGIN
			Alter Table VentaWeb
			Add Indi_Anulado Bit
		END
GO

GO
	IF NOT EXISTS(Select Top 1 1  From INFORMATION_SCHEMA.COLUMNS Where COLUMN_NAME='FechaWeb_Anulado' and TABLE_NAME='VentaWeb')	
		BEGIN
			Alter Table VentaWeb
			Add FechaWeb_Anulado SmallDateTime
		END	
GO

GO
	IF NOT EXISTS(Select Top 1 1  From INFORMATION_SCHEMA.COLUMNS Where COLUMN_NAME='Voucher' and TABLE_NAME='VentaWeb')	
		BEGIN
			Alter Table VentaWeb
			Add Voucher Varchar(50)
		END	
GO

--------------VALIDAR COLUMNAS VENTA------------------

GO
	IF NOT EXISTS(Select Top 1 1  From INFORMATION_SCHEMA.COLUMNS Where COLUMN_NAME='idModulo' and TABLE_NAME='Venta')	
		BEGIN
			Alter Table Venta
			Add idModulo Tinyint
		END	
GO

--------------VALIDAR COLUMNAS WebTarjetaPagoTipo------------------
GO
	IF NOT EXISTS(Select Top 1 1 From INFORMATION_SCHEMA.COLUMNS Where COLUMN_NAME='Flag_Pasarela' and TABLE_NAME='WebTarjetaPagoTipo')	
		BEGIN
			Alter Table WebTarjetaPagoTipo
			Add Flag_Pasarela	Varchar(5)
		END
GO

GO
	IF NOT EXISTS(Select Top 1 1 From INFORMATION_SCHEMA.COLUMNS Where COLUMN_NAME='Titulo' and TABLE_NAME='WebTarjetaPagoTipo')	
		BEGIN
			Alter Table WebTarjetaPagoTipo
			Add Titulo	Varchar(100)
		END	
GO

---------------ALTERAR TIPO DE DATO DE COLUMNA---------------
GO
ALTER TABLE WebOrders ALTER COLUMN Id_Users Int
GO

GO
ALTER TABLE Users DROP CONSTRAINT PK__Users__2C29722F
Go

GO
ALTER TABLE Users ALTER COLUMN Id_Users Int
GO

GO
ALTER TABLE Users  ADD CONSTRAINT 
PK__Users__2C29722F PRIMARY KEY (Id_Users);
Go

---------------INSERCION DE DATOS---------------
-----Tabla Insertar Datos Tb_Oficinas
GO
	If Exists (Select Top 1 1 From Tb_Oficinas Where Codi_Sucursal=999)
		Begin

			Update Tb_Oficinas 
				Set 
					Descripcion	='Web'	,
					Activo		=	0	,
					web			=	0
			Where Codi_Sucursal=999
		End
	Else
		Begin
			Insert Into Tb_Oficinas(
				Codi_Sucursal		, Descripcion		, Zona				, Orden				, 
				Sucursal			, web				, Activo			, combustible		,
				ver
			)
			Values(
				999					, 'Web'				, 0					, 0					, 
				'S'					, 0					, 0					, ''				,
				''
			)
		End


GO

-----Tabla Insertar Datos Tb_PuntoVenta
GO

	If Exists (Select Top 1 1 From Tb_PuntoVenta Where Codi_puntoVenta=999)
		Begin

			Update Tb_PuntoVenta 
				Set 
					Codi_Sucursal	= 999	,
					Descripcion		='Web'	,
					Activo			=	0	
			Where Codi_PuntoVenta=999
		End
	Else
		Begin
			Insert Into Tb_PuntoVenta(
				Codi_puntoVenta		, Codi_Sucursal		, Descripcion		, Activo			, Embarque			, sigla
			)
			Values(
				999					, 999				, 'Web'				, 0					, ''				, '' 
			)
		End


GO


-----Tabla Insertar Datos Tb_Usuario
GO
	If Exists (Select Top 1 1 From Tb_Usuario Where Codi_Usuario=999)
		Begin

			Update Tb_Usuario 
				Set 
					Codi_Sucursal	= 999	,
					Codi_puntoVenta	= 999	,
					Terminal		= 0		,
					Login			='Web'		
			Where Codi_Usuario=999
		End
	Else
		Begin
			Insert Into Tb_Usuario(
				Codi_Usuario		, Codi_Empresa			, Codi_Sucursal			, Codi_puntoVenta		, 
				Login				, Pws					, Nivel					, Terminal			
			)
			Values(
				999					, 1						, 999					, 999					, 
				'Web'				, '!"#$%&/'				, 1						, 0
			)
		End


GO

-----Tabla Insertar Datos Tb_Correlativo_Auxiliares
GO
	------Correlativo WebOrders------
	If Not Exists (Select Top 1 1 From Tb_Correlativo_Auxiliares Where TABLA='TB_RESERVAS')
		Begin
			Insert Into Tb_Correlativo_Auxiliares(
				TABLA				, CORRELATIVO			, OFICINA			, flag_venta		
			)
			Values(
				'TB_RESERVAS'		, '1'						, 999					, ''
			)
		End
GO

GO
	------Correlativo Correlativo Ventas------
	If Not Exists (Select Top 1 1 From Tb_Correlativo_Auxiliares Where TABLA='Venta_Correlativo')
		Begin
			Insert Into Tb_Correlativo_Auxiliares(
				TABLA				, CORRELATIVO			, OFICINA			, flag_venta		
			)
			Values(
				'Venta_Correlativo'	, '1'					, 999				, ''
			)
		End
GO

GO
	------Correlativo Correlativo WebOrders------
	If Not Exists (Select Top 1 1 From Tb_Correlativo_Auxiliares Where TABLA='WebOrdersPasajes')
		Begin
			Insert Into Tb_Correlativo_Auxiliares(
				TABLA				, CORRELATIVO			, OFICINA			, flag_venta		
			)
			Values(
				'WebOrdersPasajes'	, '1'					, 999				, ''
			)
		End
GO
-----Tabla Insertar Datos Tb_Correlativo_Documento
Go
DECLARE 
    @Codi_Empresa TinyInt

DECLARE xCorrelativoFact CURSOR
FOR SELECT 
        Codi_Empresa
    FROM 
        Tb_Empresa;

OPEN xCorrelativoFact;

FETCH NEXT FROM xCorrelativoFact INTO @Codi_Empresa;

WHILE @@FETCH_STATUS = 0
    BEGIN
       If Not Exists(Select * From Tb_Correlativo_Documento Where Codi_Sucursal=999 and Codi_PuntoVenta=999 
	   and Terminal=999 and Tipo='E' and Codi_Empresa=@Codi_Empresa and Codi_Documento='01' and Serie='996')
		Begin
			Insert Into Tb_Correlativo_Documento
			(
				Codi_Empresa		, Codi_Documento	, Codi_Sucursal		, Codi_PuntoVenta	, Fecha			, 
				Tipo				, Terminal			, Serie				, Numero	
			)
			Values(
				@Codi_Empresa		, '01'				, 999				, 999				, Convert(Varchar(10),getdate(),103),
				'E'					, 999				, '996'				, '0000001'
			)
		End
	  Else
		Begin
			Print 'No Generaro Correaltivo de Factura'
		End

       If Not Exists(Select * From Tb_Correlativo_Documento Where Codi_Sucursal=999 and Codi_PuntoVenta=999 
	   and Terminal=999 and Tipo='E' and Codi_Empresa=@Codi_Empresa and Codi_Documento='03' and Serie='995')
		Begin
			Insert Into Tb_Correlativo_Documento
			(
				Codi_Empresa		, Codi_Documento	,  Codi_Sucursal	, Codi_PuntoVenta	, Fecha			, 
				Tipo				, Terminal			, Serie				, Numero	
			)
			Values(
				@Codi_Empresa		, '03'				, 999				, 999				, Convert(Varchar(10),getdate(),103),
				'E'					, 999				, '995'				, '0000001'
			)
		End
	  Else
		Begin
			Print 'No Generaro Correaltivo de Boleta Ventas'
		End
	  FETCH NEXT FROM xCorrelativoFact INTO @Codi_Empresa

    END

CLOSE xCorrelativoFact

DEALLOCATE xCorrelativoFact

Go