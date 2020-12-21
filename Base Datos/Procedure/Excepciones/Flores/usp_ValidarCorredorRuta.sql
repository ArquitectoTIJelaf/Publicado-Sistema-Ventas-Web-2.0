Alter procedure Usp_Tb_Precios_Fecha_by_Servicio_Nacionalidad_Nivel_1     
@Origen   int,      
@Destino  int,      
@Hora   varchar(10),      
@Fecha   smalldatetime,      
@Servicio  tinyint,      
@empresa  tinyint,      
@Nivel   varchar(1)      
as      
SET NOCOUNT ON       
      


	IF EXISTS (SELECT top 1 Tb_Precio_Fecha.Precio_Nor,Id_Nacionalidad,Tb_Precio_Fecha.Id_Precios_Fecha FROM dbo.Tb_Precio_Fecha with (nolock)      
  INNER JOIN dbo.Tb_Servicio_Fecha ON dbo.Tb_Precio_Fecha.Id_Precios_Fecha = dbo.Tb_Servicio_Fecha.Id_Precios_Fecha       
  INNER JOIN dbo.Tb_Precios ON dbo.Tb_Servicio_Fecha.Id_precio = dbo.Tb_Precios.Id_precio       
  where Tb_Precios.CODI_sUCURSAL=@ORIGEN and Tb_Precios.codi_subruta=@destino and Tb_Servicio_Fecha.Codi_servicio=@servicio       
  and Tb_Precio_Fecha.fecha>@fecha and (Tb_Precio_Fecha.hora=@Hora or Tb_Precio_Fecha.hora='')  
  and (Tb_Precio_Fecha.nivel=@Nivel OR Tb_Precio_Fecha.nivel='G')       
  and (Tb_Precios.codi_empresa=@empresa or @empresa =0)and Tb_precio_Fecha.Id_canal=1 and tb_precio_fecha.Id_Nacionalidad=1)
	BEGIN
		  SELECT top 1 Tb_Precio_Fecha.Precio_Nor,Id_Nacionalidad,Tb_Precio_Fecha.Id_Precios_Fecha FROM dbo.Tb_Precio_Fecha with (nolock)      
		  INNER JOIN dbo.Tb_Servicio_Fecha ON dbo.Tb_Precio_Fecha.Id_Precios_Fecha = dbo.Tb_Servicio_Fecha.Id_Precios_Fecha       
		  INNER JOIN dbo.Tb_Precios ON dbo.Tb_Servicio_Fecha.Id_precio = dbo.Tb_Precios.Id_precio       
		  where Tb_Precios.CODI_sUCURSAL=@ORIGEN and Tb_Precios.codi_subruta=@destino and Tb_Servicio_Fecha.Codi_servicio=@servicio       
		  and Tb_Precio_Fecha.fecha<=@fecha and (Tb_Precio_Fecha.hora=@Hora or Tb_Precio_Fecha.hora='')  
		  and (Tb_Precio_Fecha.nivel=@Nivel OR Tb_Precio_Fecha.nivel='G')       
		  and (Tb_Precios.codi_empresa=@empresa or @empresa =0)and Tb_precio_Fecha.Id_canal=1 and tb_precio_fecha.Id_Nacionalidad=1       
		  order by Tb_Precio_Fecha.fecha,Tb_Precio_Fecha.Hora desc 

	END


