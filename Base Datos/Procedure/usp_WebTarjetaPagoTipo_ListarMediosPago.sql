Alter Procedure usp_WebTarjetaPagoTipo_ListarMediosPago
as
Set NoCount On  
 Begin  
  Select WebTarjetaPagoTipo_Id,Comision,Id_FormaPagoWeb,DescripcionWeb,Icono,
  Flag_Venta,WebTarjetaPagoTipo_Descripcion, Flag_Pasarela  , Titulo
  From WebTarjetaPagoTipo Where Estado=1 
 End  
