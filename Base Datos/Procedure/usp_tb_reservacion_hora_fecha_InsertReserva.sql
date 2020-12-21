alter Procedure [dbo].[usp_tb_reservacion_hora_fecha_InsertReserva]
@ID_VENTA			int,
@NUME_BOLETO		Varchar(11),
@FECHA_C			varchar(10),
@HORA_C				varchar(7)
as
--Set NoCount On
Begin
	Insert Into tb_reservacion_hora_fecha(ID_VENTA,NUME_BOLETO,FECHA_C,HORA_C)
	Values (@ID_VENTA,@NUME_BOLETO,@FECHA_C,@HORA_C)
End
