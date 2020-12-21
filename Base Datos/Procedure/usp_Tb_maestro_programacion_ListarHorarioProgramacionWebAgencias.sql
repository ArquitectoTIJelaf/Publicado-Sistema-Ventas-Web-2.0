alter Procedure usp_Tb_maestro_programacion_ListarHorarioProgramacionWebAgencias   
@Origen   int,  
@Destino  int,  
@Codi_Servicio int,  
@Empresa tinyint  
as  
SET NOCOUNT ON  
 select mp.Hora,rm.Codi_Sucursal,rm.Codi_Destino,ser.codi_plantilla, ri1.Dias,mp.Nro_Viaje,  
 rm.Codi_Servicio,ser.Descripcion as Servicio,ori.Descripcion as Origen,des.Descripcion as Destino,  
 ri1.Hora_Paso as Hora_Partida,mp.Codi_Empresa,mp.St_Opcional  from Tb_Maestro_Programacion mp  with (nolock)  
 Inner Join Tb_Ruta_Maestro rm on rm.Nro_Ruta=mp.Nro_Ruta and  (rm.Codi_servicio=@Codi_Servicio  or @Codi_Servicio=0)
 Inner Join Tb_Ruta_Intermedio ri1 on mp.Nro_Viaje=ri1.Nro_Viaje and ri1.Codi_Sucursal=@Origen and ri1.Virtual='1'  
 Inner Join Tb_Ruta_Intermedio ri2 on mp.Nro_Viaje=ri2.Nro_Viaje and ri2.Codi_Sucursal=@Destino and ri2.Virtual='1'  
 Inner Join Tb_Servicio ser on rm.Codi_Servicio=ser.Codi_Servicio  
 Inner Join Tb_Oficinas ori on ri1.Codi_Sucursal=ori.Codi_Sucursal  
 Inner Join Tb_Oficinas des on ri2.Codi_Sucursal=des.Codi_Sucursal  
 Where Tipo_V='1' and Tipo_E='1' and ri1.Codi_Sucursal<>ri2.Codi_Sucursal and mp.TR<>1  
 and ri1.Orden<ri2.Orden and (mp.Codi_Empresa=@Empresa or @Empresa=0) 
  order by CAST(ri1.Hora_Paso as smalldatetime) 