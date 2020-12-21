alter Procedure usp_Info_TransaccionVisaNet_Grabar
@Id_WebOrders			int,
@Nro_Orden				Int,
@Importe_Autorizado		Real,
@NumTarjetaHabiente		Varchar(200),
@Fecha_Compra			SmallDateTime,
@Respuesta				Varchar(3),
@Cod_Accion				Varchar(5),
@Mensaje_Error			Varchar(250),
@NomTarjeta				Varchar(100),
@Transaccion_Id			Varchar(20)
as
	Begin Transaction
		Declare @Estado_Transaccion		Varchar(50)
		
		If @Respuesta='1' Or @Respuesta='000'
			Set @Estado_Transaccion='Autorizada'
		Else
			Set @Estado_Transaccion='Denegada'

		Insert Into Tb_Info_TransaccionVisaNet(
			Id_WebOrders		 ,
			Nro_Orden			 ,
			Importe_Autorizado	 ,
			NumTarjetaHabiente	 ,
			Fecha_Compra		 ,
			Estado_Transaccion	 ,
			Cod_Accion			 ,
			Mensaje_Error		 ,
			NomTarjeta			 ,
			Transaccion_Id	 	 ,
			Trans_Fraude
		
		)
		Values(
			@Id_WebOrders		 ,
			@Nro_Orden			 ,
			@Importe_Autorizado	 ,
			@NumTarjetaHabiente	 ,
			@Fecha_Compra		 ,
			@Estado_Transaccion	 ,
			@Cod_Accion			 ,
			@Mensaje_Error		 ,
			@NomTarjeta			 ,
			@Transaccion_Id	 	 ,
			0
		)

	If @@ERROR<>0
		RollBack Transaction
	Else
		Commit Transaction