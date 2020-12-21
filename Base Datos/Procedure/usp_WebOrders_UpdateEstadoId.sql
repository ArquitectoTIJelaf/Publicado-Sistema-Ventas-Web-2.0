alter procedure usp_WebOrders_UpdateEstadoId
@Id_WebOrders	Int,
@Estado			int
as  

Set NoCount On  

	Begin Transaction  
		update weborders set  Estado_WebOrders=@Estado where Id_WebOrders=@Id_WebOrders  

		update Tem_compra
		Set indicador=@Estado
		Where NroOrden in(select cast (NumOrder_WebOrders as Int) From WebOrders Where Id_WebOrders=@Id_WebOrders)


	
	If @@Error<>0  
	   RollBack Transaction                  
	Else  
	   Commit TRANSACTION 