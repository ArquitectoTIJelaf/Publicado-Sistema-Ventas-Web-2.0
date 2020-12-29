GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_WebOrders_Exists_Orders')
	BEGIN
		DROP PROCEDURE usp_WebOrders_Exists_Orders
	End
GO
GO
Create Procedure usp_WebOrders_Exists_Orders
@IdWebOders   int  
as
SET NOCOUNT ON 
	select Id_WebOrders,Id_Users,WOrderType_Id,Estado_WebOrders from WebOrders   
	where Id_WebOrders=@IdWebOders AND Estado_WebOrders=8


