GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Tb_Fecha_Eventual_Validacion')
	BEGIN
		DROP PROCEDURE usp_Tb_Fecha_Eventual_Validacion
	End
GO
GO
Create Procedure usp_Tb_Fecha_Eventual_Validacion
@Nro_viaje		int,
@Fecha			smalldatetime
as
Set Nocount on
	Begin
		Select Nro_viaje from Tb_Fecha_Eventual Where Nro_viaje=@Nro_viaje and Fecha=@Fecha
		union
		select nro_viaje from Tb_Calendarios_Dias_Viaje 
		where Lunes=1 and  DATENAME(dw,@Fecha)='Lunes' AND Nro_viaje=@Nro_viaje
		or Martes=1 and  DATENAME(dw,@Fecha)='Martes'  AND Nro_viaje=@Nro_viaje
		or Miercoles=1 and  DATENAME(dw,@Fecha)='Miercoles'  AND Nro_viaje=@Nro_viaje
		or Jueves=1 and  DATENAME(dw,@Fecha)='Jueves'  AND Nro_viaje=@Nro_viaje
		or Viernes=1 and  DATENAME(dw,@Fecha)='Viernes'  AND Nro_viaje=@Nro_viaje
		or Sabado=1 and  DATENAME(dw,@Fecha)='Sabado'  AND Nro_viaje=@Nro_viaje
		or Domingo=1 and  DATENAME(dw,@Fecha)='Domingo'  AND Nro_viaje=@Nro_viaje
	End