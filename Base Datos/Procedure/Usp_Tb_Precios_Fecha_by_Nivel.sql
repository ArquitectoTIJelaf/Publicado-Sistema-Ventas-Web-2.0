Alter procedure Usp_Tb_Precios_Fecha_by_Nivel
@Origen			int,
@Destino		int,
@Hora			varchar(10),
@Fecha			smalldatetime,
@Servicio		tinyint,
@Nivel			varchar(1),
@empresa		tinyint,
@IdNacionalidad	tinyint
as
SET NOCOUNT ON
			
SELECT top 1  Tb_Precio_Fecha.Precio_Nor,Tb_Servicio_Fecha.id_precios_fecha
 FROM dbo.Tb_Precio_Fecha with (nolock)
INNER JOIN dbo.Tb_Servicio_Fecha ON dbo.Tb_Precio_Fecha.Id_Precios_Fecha = dbo.Tb_Servicio_Fecha.Id_Precios_Fecha 
INNER JOIN dbo.Tb_Precios ON dbo.Tb_Servicio_Fecha.Id_precio = dbo.Tb_Precios.Id_precio 
where Tb_Precios.CODI_sUCURSAL=@ORIGEN and Tb_Precios.codi_subruta=@destino and Tb_Servicio_Fecha.Codi_servicio=@servicio 
and Tb_Precio_Fecha.fecha<=@fecha and isnull(Tb_Precio_Fecha.hora,'')=@Hora and (Tb_Precio_Fecha.nivel=@nivel OR Tb_Precio_Fecha.nivel='G') 
and (Tb_Precios.codi_empresa=@empresa or @empresa=0)and Tb_precio_Fecha.Id_canal='1' and tb_precio_fecha.Id_Nacionalidad=@IdNacionalidad order by fecha desc

