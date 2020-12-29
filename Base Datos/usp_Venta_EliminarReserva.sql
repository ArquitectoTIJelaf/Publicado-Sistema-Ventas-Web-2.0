GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Venta_EliminarReserva')
	BEGIN
		DROP PROCEDURE usp_Venta_EliminarReserva
	End
GO
GO
Create Procedure usp_Venta_EliminarReserva
@Id_WebOrders		Int
as
	Begin
		Begin Transaction
			Declare @TableVentaWeb Table (Id_VentaWeb Int)
			Insert Into @TableVentaWeb(Id_VentaWeb)
			Select Id_ventaWeb From Venta Where Id_VentaWeb in(
			Select Id_VentaWeb From VentaWeb Where Id_WebOrders=@Id_WebOrders /*and OrderVenta=@Tipo*/)
			and FLAG_VENTA='R'

					Delete From TB_RESERVACION_HORA_FECHA Where ID_VENTA in(
						Select ID_VENTA From Venta Where Id_VentaWeb in(
							Select Id_VentaWeb From @TableVentaWeb
						) and FLAG_VENTA='R'
					)

					Update VENTA
					Set
						INDI_ANULADO='T',
						FECH_ANULACION=CONVERT(Varchar(10),getdate(),103)
					Where Id_VentaWeb in(
							Select Id_VentaWeb From @TableVentaWeb
					) and FLAG_VENTA='R'

		If @@ERROR<>0
			Begin
				RollBack Transaction
			End
		Else
			Begin
				Commit Transaction
			End
	End
