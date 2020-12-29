GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_GenerarCorrelativoFEW')
	BEGIN
		DROP PROCEDURE usp_GenerarCorrelativoFEW
	End
GO
GO
Create Procedure usp_GenerarCorrelativoFEW
@Codi_Empresa		Tinyint,
@Ruc_Cliente		Varchar(20)
as
	Declare @Codi_Documento	Varchar(2)
	Declare @Serie_Documento	Varchar(5)
	Declare @Numero_Documento	Varchar(10)
	Set @Codi_Documento='03'
	If LTRIM(RTRIM(@Ruc_Cliente))<>''
		Begin
			Set @Codi_Documento='01'
		End
	Select @Serie_Documento=Serie,@Numero_Documento=Numero from Tb_Correlativo_Documento
	Where Codi_Empresa=@Codi_Empresa and Codi_Documento=@Codi_Documento 
	and Codi_Sucursal='999' and Codi_PuntoVenta='999' and Terminal='999' and Tipo='E'

	--Update Tb_Correlativo_Documento
	--Set Numero=Right(('000000'+Cast((Cast(Numero as Int)+1)as Varchar)),7)
	--Where Codi_Empresa=@Codi_Empresa and Codi_Documento=@Codi_Documento 
	--and Codi_Sucursal='999' and Codi_PuntoVenta='999' and Terminal='999' and Tipo='E'
	--and Serie=@Serie_Documento

	Select @Serie_Documento as Serie,@Numero_Documento As Numero, @Codi_Documento as Codi_Documento


