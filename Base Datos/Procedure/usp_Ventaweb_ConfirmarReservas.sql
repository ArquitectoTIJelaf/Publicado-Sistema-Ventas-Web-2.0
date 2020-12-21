Alter Procedure usp_Ventaweb_ConfirmarReservas
@Id_WebOrders		int,
@Voucher			Varchar(50)
as
Begin
	Begin Transaction 
		Update VentaWeb Set Flag_Venta='T',Voucher=@Voucher Where Id_Weborders=@Id_WebOrders
		Update Venta set Flag_Venta='T' Where Id_VentaWeb in(Select Id_VentaWeb From VentaWeb Where Id_Weborders=@Id_WebOrders)	
	If @@error<>0
		Rollback Transaction
	else
		Commit Transaction
End


