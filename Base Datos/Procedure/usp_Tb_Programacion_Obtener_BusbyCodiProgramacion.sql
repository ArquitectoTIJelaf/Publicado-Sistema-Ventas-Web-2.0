Alter Procedure usp_Tb_Programacion_Obtener_BusbyCodiProgramacion
@Codi_Programacion		int
as
Set nocount on
Begin
	select p.Codi_bus,b.Plan_bus,isnull(bp.Nro_Poliza,'')Nro_Poliza,
	Isnull(convert(varchar(10),bp.Fecha_Reg,103),'')Fecha_Reg,
	Isnull(convert(varchar(10),bp.Fecha_Ven,103),'')Fecha_Ven,   
	Isnull(bp.estado,'0')Estado from Tb_Programacion p with(nolock)
	Inner Join Tb_Bus b on p.Codi_Bus=b.Codi_Bus
	Left Join Tb_Bus_Poliza bp on b.Codi_Bus=bp.Codi_Bus
	Where Codi_Programacion=@Codi_Programacion
End