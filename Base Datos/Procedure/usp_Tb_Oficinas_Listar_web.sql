ALter procedure usp_Tb_Oficinas_Listar_web
as
SET NOCOUNT ON
		  	SELECT   Codi_Sucursal, Descripcion

			FROM         Tb_Oficinas with (nolock)

			Where Activo=1 and Web='1' and Sucursal='S'