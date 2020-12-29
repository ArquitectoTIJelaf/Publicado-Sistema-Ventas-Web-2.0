GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Tb_Oficinas_Listar_web')
	BEGIN
		DROP PROCEDURE usp_Tb_Oficinas_Listar_web
	End
GO
GO
Create procedure usp_Tb_Oficinas_Listar_web
as
SET NOCOUNT ON
		  	SELECT   Codi_Sucursal, Descripcion

			FROM         Tb_Oficinas with (nolock)

			Where Activo=1 and Web='1' and Sucursal='S'