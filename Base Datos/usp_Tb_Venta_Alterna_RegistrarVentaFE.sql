GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Tb_Venta_Alterna_RegistrarVentaFE')
	BEGIN
		DROP PROCEDURE usp_Tb_Venta_Alterna_RegistrarVentaFE
	End
GO
GO
Create Procedure usp_Tb_Venta_Alterna_RegistrarVentaFE
@id_venta				int,
@Codi_programacion_Alt	int,
@Origen_Alt				smallint,
@SubRuta_Alt			smallint,
@Correlativo			int,
@Codi_Ruta				smallint
as

Begin
	Declare @Documento_Venta		Varchar(15)
	Select @Documento_Venta=NUME_BOLETO From VENTA_DERIVADA
	Where id_venta=@id_venta
	Insert Into Tb_Venta_Alterna(id_venta,Codi_programacion_Alt,Origen_Alt,SubRuta_Alt,NUme_Boleto,Correlativo,Codi_Ruta)
	Values(@id_venta,@Codi_programacion_Alt,@Origen_Alt,@SubRuta_Alt,@Documento_Venta,@Correlativo,@Codi_Ruta)
End


