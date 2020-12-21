ALter Procedure usp_Tb_Viaje_Programacion_Update_Web
@Nro_Viaje			Int,
@Codi_programacion	Int,
@Fecha				smalldatetime,
@Codi_Bus			VarChar(5),
@IDCID				Varchar(50),
@Id_User			Int
as
Insert Into Tb_Viaje_Programacion (Nro_Viaje,Codi_programacion,Fecha,N_Asiento,St,Codi_Bus)
Values(@Nro_Viaje,@Codi_programacion,@Fecha,0,'1',@Codi_Bus)