GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Venta_AnularOrdenWeb')
	BEGIN
		DROP PROCEDURE usp_Venta_AnularOrdenWeb
	End
GO
GO
Create Procedure usp_Venta_AnularOrdenWeb
@Id_webOrders Int
as
	Begin
		Begin Transaction

			Update VENTA
			Set Indi_Anulado='T',Fech_Anulacion=convert(Varchar(10),getdate(),103)
			Where Id_VentaWeb in(
				Select Id_VentaWeb From VentaWeb Where Id_WebOrders=@Id_webOrders
			)

			update ventaweb Set Indi_Anulado=1,costo=0,asientod=0 Where Id_WebOrders=@Id_webOrders


		If @@ERROR<>0
			RollBack Transaction
		Else
			Commit Transaction
	End 