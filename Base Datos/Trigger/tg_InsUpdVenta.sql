Create TRIGGER tg_InsUpdVenta
ON Venta_Derivada
AFTER  INSERT, UPDATE
AS
	BEGIN
		Declare @Id_venta					Int,
				@Id_VentaWeb				Int,
				@Serie_Boleto				SmallInt,
				@Numero_Boleto				Int,
				@Tipo						Char(1),
				@Codi_Empresa				Tinyint,
				@FechaViaje					Varchar(10),
				@FechaVenta					Varchar(10),
				@FechaRegistro				SmallDatetime,
				@HoraProgamacion			Varchar(10),
				@HoraViaje					Varchar(10),
				@HoraEmbarque				Varchar(10),
				@Codi_programacion			Int,
				@Codi_Usuario				SmallInt,
				@Codi_Origen				SmallInt,
				@Codi_Destino				SmallInt,
				@Codi_Servicio				Tinyint,
				@Codi_Embarque				SmallInt,
				@Codi_Arribo				SmallInt,
				@Precio						Real,
				@Pasajero					Varchar(200),
				@Dni						Varchar(20),
				@Ruc						Varchar(11),
				@Asiento					Tinyint,
				@Indi_Anulado				VarChar(3),
				@Fecha_Anulacion			SmallDatetime,
				@Codi_Usuario_Anu			SmallInt,
				@Flag_Venta					Varchar(3),
				@Codi_Sucursal				SmallInt,
				@Codi_PuntoVenta			SmallInt,
				@TipoMovimiento				Varchar(200)

		Select 
		@Id_venta			=	inserted.id_venta,
		@FechaViaje			=	Convert(Varchar(10),inserted.Fecha_Viaje,103),
		@HoraViaje			=	inserted.Hora_Viaje,
		@HoraEmbarque		=	inserted.Hora_Embarque_Web,
		@Codi_Servicio		=	inserted.Servicio,
		@Codi_Embarque		=	inserted.Sube_En,
		@Codi_Arribo		=	inserted.baja_en
		from inserted

		Select 
		@Id_VentaWeb		=	VENTA.Id_VentaWeb,
		@Serie_Boleto		=	VENTA.SERIE_BOLETO,
		@Numero_Boleto		=	VENTA.NUME_BOLETO,
		@Tipo				=	VENTA.tipo,
		@Codi_Empresa		=	VENTA.CODI_EMPRESA,
		@FechaVenta			=	Convert(Varchar(10),VENTA.FECH_VENTA,103),
		@FechaRegistro		=	getdate(),
		@HoraProgamacion	=	(Case When VENTA.CODI_PROGRAMACION>0 Then (Select Hora_programacion From Tb_Programacion Where Tb_Programacion.Codi_Programacion=VENTA.CODI_PROGRAMACION) Else '' End),
		@Codi_programacion	=	VENTA.CODI_PROGRAMACION,
		@Codi_Usuario		=	VENTA.CLAV_USUARIO,
		@Codi_Origen		=	VENTA.cod_origen,
		@Codi_Destino		=	VENTA.CODI_SUBRUTA,
		@Precio				=	VENTA.PREC_VENTA,
		@Pasajero			=	VENTA.NOMBRE,
		@Dni				=	VENTA.DNI,
		@Ruc				=	VENTA.NIT_CLIENTE,
		@Asiento			=	VENTA.NUME_ASIENTO,
		@Indi_Anulado		=	VENTA.INDI_ANULADO,
		@Fecha_Anulacion	=	VENTA.FECH_ANULACION,
		@Codi_Usuario_Anu	=	VENTA.clav_usuario1,
		@Flag_Venta			=	VENTA.FLAG_VENTA,
		@Codi_Sucursal		=	VENTA.CODI_SUCURSAL,
		@Codi_PuntoVenta	=	VENTA.Punto_Venta,
		@TipoMovimiento		=	Case When Exists(Select Top 1 1 From Tb_Auditoria_Venta Where Tb_Auditoria_Venta.Id_venta=@Id_venta) Then 'Modificacion' Else 'Insercion' End
		from VENTA	Where id_venta=@Id_venta


		
		Insert Into Tb_Auditoria_Venta
		(
			Id_venta			,
			Id_VentaWeb			,
			Serie_Boleto		,
			Numero_Boleto		,
			Tipo				,
			Codi_Empresa		,
			FechaViaje			,
			FechaVenta			,
			FechaRegistro		,
			HoraProgamacion		,
			HoraViaje			,
			HoraEmbarque		,
			Codi_programacion	,
			Codi_Usuario		,
			Codi_Origen			,
			Codi_Destino		,
			Codi_Servicio		,
			Codi_Embarque		,
			Codi_Arribo			,
			Precio				,
			Pasajero			,
			Dni					,
			Ruc					,
			Asiento				,
			Indi_Anulado		,
			Fecha_Anulacion		,
			Codi_Usuario_Anu	,
			Flag_Venta			,
			Codi_Sucursal		,
			Codi_PuntoVenta		,
			TipoMovimiento		
		)
		Values
		(
			@Id_venta			,
			@Id_VentaWeb		,
			@Serie_Boleto		,
			@Numero_Boleto		,
			@Tipo				,
			@Codi_Empresa		,
			@FechaViaje			,
			@FechaVenta			,
			@FechaRegistro		,
			@HoraProgamacion	,
			@HoraViaje			,
			@HoraEmbarque		,
			@Codi_programacion	,
			@Codi_Usuario		,
			@Codi_Origen		,
			@Codi_Destino		,
			@Codi_Servicio		,
			@Codi_Embarque		,
			@Codi_Arribo		,
			@Precio				,
			@Pasajero			,
			@Dni				,
			@Ruc				,
			@Asiento			,
			@Indi_Anulado		,
			@Fecha_Anulacion	,
			@Codi_Usuario_Anu	,
			@Flag_Venta			,
			@Codi_Sucursal		,
			@Codi_PuntoVenta	,
			@TipoMovimiento
		)
	END