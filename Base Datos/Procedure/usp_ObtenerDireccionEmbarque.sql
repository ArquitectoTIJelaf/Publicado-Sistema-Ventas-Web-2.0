alter Procedure usp_ObtenerDireccionEmbarque
@Codi_Empresa		Tinyint,
@Codi_Embarque		SmallInt
as
	SELECT pv.Descripcion Direccion FROM Tb_Direccion_Empresa de
	Inner Join Tb_PuntoVenta pv on de.codi_sucursal=pv.Codi_puntoVenta
	WHERE de.codi_empresa=@Codi_Empresa AND de.codi_sucursal=@Codi_Embarque