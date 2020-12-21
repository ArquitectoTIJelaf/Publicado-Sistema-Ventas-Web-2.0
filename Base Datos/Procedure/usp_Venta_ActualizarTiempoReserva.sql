alter Procedure usp_Venta_ActualizarTiempoReserva
@Id_WebOrders		Int,
@FechaReserva		Varchar(10),
@HoraReserva		Varchar(10)
as
	Begin
		Begin Transaction

					Update TB_RESERVACION_HORA_FECHA 
						Set HORA_C	= @HoraReserva,
							FECHA_C	= @FechaReserva
					Where ID_VENTA in(
						Select ID_VENTA From Venta Where Id_VentaWeb in(
							Select Id_VentaWeb From VentaWeb Where Id_WebOrders=@Id_WebOrders
						) and FLAG_VENTA='R' and Indi_Anulado='F'
					)


		If @@ERROR<>0
			Begin
				RollBack Transaction
			End
		Else
			Begin
				Commit Transaction
			End		
	End

