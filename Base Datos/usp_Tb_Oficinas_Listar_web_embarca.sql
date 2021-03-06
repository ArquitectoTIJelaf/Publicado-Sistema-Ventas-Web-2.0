GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Tb_Oficinas_Listar_web_embarca')
	BEGIN
		DROP PROCEDURE usp_Tb_Oficinas_Listar_web_embarca
	End
GO
GO
Create procedure [usp_Tb_Oficinas_Listar_web_embarca]  
as  
SET NOCOUNT ON  
SELECT  Codi_puntoVenta as Codi_Sucursal, Tb_PuntoVenta.Descripcion  
FROM    Tb_Oficinas with (nolock)  
inner join Tb_PuntoVenta on Tb_Oficinas.Codi_Sucursal=Tb_PuntoVenta.Codi_Sucursal  
Where Tb_Oficinas.Activo=1 and Web='1' ORDER BY Descripcion 
