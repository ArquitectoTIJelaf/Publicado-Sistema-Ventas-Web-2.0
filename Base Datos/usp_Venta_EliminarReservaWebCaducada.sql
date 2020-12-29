GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Venta_EliminarReservaWebCaducada')
	BEGIN
		DROP PROCEDURE usp_Venta_EliminarReservaWebCaducada
	End
GO
GO
Create Procedure usp_Venta_EliminarReservaWebCaducada
as
Begin

	Begin Transaction

		If Exists(Select Top 1 1 From Venta v
		Inner Join TB_RESERVACION_HORA_FECHA rfh on v.id_venta=rfh.ID_VENTA
		where v.FLAG_VENTA='R' and v.CODI_SUCURSAL=999 and isnull(v.Id_VentaWeb,0)>0 and(rfh.Fecha_C<>'' and rfh.HORA_C<>'' and rfh.HORA_C<>'__:___M') 
		and cast(rfh.Fecha_C + ' ' + rfh.hora_c as DateTime) <getdate())
			Begin

				Declare @TableVentaWeb	Table (Id_VentaWeb	Int)
				Insert Into @TableVentaWeb (Id_VentaWeb)
				Select v.Id_VentaWeb From Venta v
				Inner Join TB_RESERVACION_HORA_FECHA rfh on v.id_venta=rfh.ID_VENTA
				where v.FLAG_VENTA='R' and v.CODI_SUCURSAL=999 and isnull(v.Id_VentaWeb,0)>0 and(rfh.Fecha_C<>'' and rfh.HORA_C<>'' and rfh.HORA_C<>'__:___M') 
				and cast(rfh.Fecha_C + ' ' + rfh.hora_c as DateTime) <getdate()

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

			End
	If @@ERROR<>0
		Begin
			RollBack Transaction
		End
	Else
		Begin
			Commit Transaction
		End
End