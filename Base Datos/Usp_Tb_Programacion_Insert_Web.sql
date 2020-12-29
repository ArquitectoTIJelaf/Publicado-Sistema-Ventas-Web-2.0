GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'Usp_Tb_Programacion_Insert_Web')
	BEGIN
		DROP PROCEDURE Usp_Tb_Programacion_Insert_Web
	End
GO
GO
Create procedure Usp_Tb_Programacion_Insert_Web
@Nro_Viaje				Int,
@codi_Empresa			tinyint,
@Codi_sucursal			smallint,
@Codi_Ruta				smallint,
@Codi_bus				varchar(5),
@fecha					smalldatetime,
@hora					char(7),
@codi_servicio			tinyint,
@Nro_Programacion		Int Output
as
  Begin Transaction   
	Declare @Codi_programacion		int

	Select @Codi_programacion=Cast(CORRELATIVO as Int) From Tb_Correlativo_Auxiliares 
	where TABLA='TB_PROGRAMACION' and Oficina='999'

	Select @Nro_Programacion=Max(codi_programacion)
	From Tb_Programacion
	If @Nro_Programacion>=@Codi_programacion
		Begin
			Set @Codi_programacion=@Nro_Programacion+1
			
			Update Tb_Correlativo_Auxiliares 
			Set CORRELATIVO=@Codi_programacion+1
			where TABLA='TB_PROGRAMACION' and Oficina='999'
		End
	
	Insert Into tb_programacion
	(codi_programacion,codi_Empresa,codi_sucursal,codi_ruta,codi_bus,fech_programacion,hora_programacion,codi_servicio,CODI_chofer,codi_copiloto,codi_terramoza)
	values
	(@Codi_programacion,@codi_Empresa,@Codi_sucursal,@Codi_Ruta,@codi_bus,@fecha,@hora,@codi_servicio,'00000','00000','00000')

	Insert Into Tb_Viaje_Programacion (Nro_Viaje,Codi_programacion,Fecha,N_Asiento,St,Codi_Bus)
	Values(@Nro_Viaje,@Codi_programacion,@Fecha,0,'1',@Codi_Bus)

	Set @Nro_Programacion=@Codi_programacion

	If @@Error<>0
        RollBack Transaction                
	Else
		Commit Transaction