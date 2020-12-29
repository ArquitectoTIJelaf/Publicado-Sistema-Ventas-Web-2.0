GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'Usp_Tb_Viaje_Historial_Verifica_Turno')
	BEGIN
		DROP PROCEDURE Usp_Tb_Viaje_Historial_Verifica_Turno
	End
GO
GO
Create PROCEDURE Usp_Tb_Viaje_Historial_Verifica_Turno 
@Nro_Viaje			INT,
@FechaProgramacion	smalldatetime
as
SET NOCOUNT ON
begin
	SELECT vh.Codi_Servicio,ser.Descripcion as Servicio,vh.Codi_Empresa,e.Ruc,e.Razon_Social from Tb_Viaje_Historial vh
	Inner Join Tb_servicio ser on vh.Codi_servicio=ser.Codi_servicio 
	Inner Join Tb_Empresa e on vh.Codi_Empresa=e.Codi_Empresa
	Where Nro_Viaje=@Nro_Viaje and Fecha=@FechaProgramacion 
End