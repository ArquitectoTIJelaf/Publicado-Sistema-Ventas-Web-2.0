alter Procedure usp_tb_ListarPuntosArribo    
@Ori int,      
@des int,      
@ser int,      
@emp int,      
@pv  int,  
@hora varchar(7)  
as      
set nocount on      
begin      
SELECT DISTINCT      
 cod_paso as Codi_puntoVenta ,(select descripcion from tb_oficinas o where o.Codi_Sucursal=cod_paso) + '    HORA DESEMB. '+ hora_PASO  as PuntoVenta,
 hora_PASO,
 (Select direccion From Tb_Direccion_Empresa Where codi_empresa=@emp and codi_sucursal=cod_paso ) Direccion
FROM       
dbo.Tb_Puntos_Det INNER JOIN      
dbo.Tb_Puntos ON dbo.Tb_Puntos_Det.id_Puntos = dbo.Tb_Puntos.id_Puntos INNER JOIN      
dbo.Tb_Ruta_Maestro ON dbo.Tb_Puntos.nro_ruta = dbo.Tb_Ruta_Maestro.NRO_RUTA  AND       
dbo.Tb_Ruta_Maestro.CODI_SUCURSAL = @ORI AND       
dbo.Tb_Ruta_Maestro.CODI_DESTINO = @DES AND       
dbo.Tb_Ruta_Maestro.CODI_SERVICIO = @SER AND       
dbo.Tb_Puntos.cod_empresa = @EMP AND       
dbo.Tb_Puntos.cod_pventa = @PV AND       
dbo.Tb_Puntos.hora = @HORA AND      
Tb_Puntos_Det.tipo='2'      
end
