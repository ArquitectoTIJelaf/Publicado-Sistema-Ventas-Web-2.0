GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_ListarPrecioxAsiento')
	BEGIN
		DROP PROCEDURE usp_ListarPrecioxAsiento
	End
GO
GO
Create Procedure usp_ListarPrecioxAsiento
@Codi_Empresa		Tinyint,
@Codi_Origen		SmallInt,
@Codi_Destino		SmallInt,
@Codi_Servicio		SmallInt,
@Hora				Varchar(10),
@FechaViaje			Varchar(10),
@NumAsiento			Smallint
as
	Select Top 1 Tipo,Monto from tb_BF_Promo_Asiento pa
	Inner Join  Tb_BF_Det_Promo dp On pa.IdPromocion = dp.IdPromocion
	Inner Join Tb_BF_Asientos a on dp.IdDetallePromocion=a.IdDetallePromocion
	Where pa.Codi_Empresa=@Codi_Empresa and dp.Codi_Origen=@Codi_Origen and dp.Codi_Destino=@Codi_Destino
	and dp.Codi_Servicio=@Codi_Servicio and dp.Hora=@Hora and dp.Estado=1 and pa.Estado=1
	and a.NumAsiento	=@NumAsiento and (pa.FechaInicio<=@FechaViaje 
	and pa.FechaFin>=@FechaViaje)
