GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Tb_Viaje_Programacion_Update_Web')
	BEGIN
		DROP PROCEDURE usp_Tb_Viaje_Programacion_Update_Web
	End
GO
GO
Create Procedure usp_Tb_Viaje_Programacion_Update_Web
@Nro_Viaje			Int,
@Codi_programacion	Int,
@Fecha				smalldatetime,
@Codi_Bus			VarChar(5),
@IDCID				Varchar(50),
@Id_User			Int
as
Insert Into Tb_Viaje_Programacion (Nro_Viaje,Codi_programacion,Fecha,N_Asiento,St,Codi_Bus)
Values(@Nro_Viaje,@Codi_programacion,@Fecha,0,'1',@Codi_Bus)