GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_WebOrders_ValidarVenta')
	BEGIN
		DROP PROCEDURE usp_WebOrders_ValidarVenta
	End
GO
GO
Create Procedure usp_WebOrders_ValidarVenta
@NumOrden		Varchar(20)
as
	Select Top 1 1 From WebOrders wo
	Inner Join VentaWeb vw on wo.Id_WebOrders=vw.Id_WebOrders
	Inner Join VENTA v on vw.Id_VentaWeb=v.id_venta
	Where wo.NumOrder_WebOrders=@NumOrden and wo.Estado_WebOrders=8
