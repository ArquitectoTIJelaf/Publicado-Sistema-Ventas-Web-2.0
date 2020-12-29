GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_WebTarjetaPagoTipo_ListarMediosPago')
	BEGIN
		DROP PROCEDURE usp_WebTarjetaPagoTipo_ListarMediosPago
	End
GO
GO
Create Procedure usp_WebTarjetaPagoTipo_ListarMediosPago
as
Set NoCount On  
 Begin  
  Select WebTarjetaPagoTipo_Id,Comision,Id_FormaPagoWeb,DescripcionWeb,Icono,
  Flag_Venta,WebTarjetaPagoTipo_Descripcion, Flag_Pasarela  , Titulo
  From WebTarjetaPagoTipo Where Estado=1 
 End  

Go