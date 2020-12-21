Create Procedure usp_Tb_Programacion_ObtenerDatos
@Codi_Programacion		Int
as
	Select p.Codi_Servicio,ser.Descripcion as Servicio,
	p.Codi_Empresa,e.Ruc,e.Razon_Social  From Tb_Programacion p
	Inner Join Tb_Servicio ser on p.Codi_Servicio=ser.Codi_Servicio
	Inner Join Tb_Empresa e on p.Codi_Empresa=e.Codi_Empresa
	Where p.Codi_Programacion=@Codi_Programacion