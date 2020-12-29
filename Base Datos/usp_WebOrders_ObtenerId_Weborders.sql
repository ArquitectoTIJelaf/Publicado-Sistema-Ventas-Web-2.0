GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_WebOrders_ObtenerId_Weborders')
	BEGIN
		DROP PROCEDURE usp_WebOrders_ObtenerId_Weborders
	End
GO
GO
Create Procedure usp_WebOrders_ObtenerId_Weborders
@NumOrden		Varchar(20)
as
	Select wo.Id_WebOrders,wo.Email_Cliente,cast(Max(vw.OrderVenta)as Int) OrderVenta From WebOrders wo
	Inner Join VentaWeb vw on wo.Id_WebOrders=vw.Id_WebOrders
	Where NumOrder_WebOrders=@NumOrden
	Group By wo.Id_WebOrders,wo.Email_Cliente



