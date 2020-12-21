alter Procedure usp_Tb_Bus_Estandar_Obtener_Bus
@Codi_Empresa		Tinyint,
@Codi_Sucursal		Smallint,
@Codi_Destino		Smallint,
@Codi_Servicio		Smallint,
@Hora				Varchar(7)
as

    select bus as Codi_bus,b.Plan_bus,isnull(bp.Nro_Poliza,'')Nro_Poliza,
    Isnull(convert(varchar(10),bp.Fecha_Reg,103),'')Fecha_Reg,
    Isnull(convert(varchar(10),bp.Fecha_Ven,103),'')Fecha_Ven ,
    Isnull(bp.estado,'0')Estado from Tb_Bus_Estandar be with(nolock)
    Inner Join Tb_Bus b on be.bus=b.Codi_Bus
    Left Join Tb_Bus_Poliza bp on b.Codi_Bus=bp.Codi_Bus
    Where 
    be.Codi_Empresa=@Codi_Empresa and 
    (be.Sucursal=@Codi_Sucursal or @Codi_Sucursal=0) and 
    (be.Servicio=@Codi_Servicio or @Codi_Servicio=0) and 
    (ISNULL(be.hora,'')=@HOra) and 
    (be.Destino=@Codi_Destino or @Codi_Destino=0)