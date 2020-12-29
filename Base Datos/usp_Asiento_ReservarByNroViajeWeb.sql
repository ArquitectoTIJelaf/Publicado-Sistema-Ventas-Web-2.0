GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Asiento_ReservarByNroViajeWeb')
	BEGIN
		DROP PROCEDURE usp_Asiento_ReservarByNroViajeWeb
	End
GO
GO
Create PROCEDURE usp_Asiento_ReservarByNroViajeWeb
@NroViaje 		int,
@IdNAsiento		varchar(2),
@IdCID			varchar(50),
@Costo			Numeric(9,2),
@Fecha			smalldatetime
as
  SET TRANSACTION ISOLATION LEVEL READ COMMITTED
  Begin Transaction     
  
		IF NOT EXISTS(SELECT TOP 1 1 FROM Asiento where CODI_PROGRAMACION=@NroViaje AND NUME_ASIENTO=@IdNAsiento and Fecha=@Fecha)
			BEGIN
				insert into asiento  
				(CODI_PROGRAMACION,NUME_ASIENTO,Costo,CODI_TERMINAL,IdCID,Id_Users, TypeApp_Id,fecha,t_Ruta,DateTimeUpdated) 
				values(@NroViaje  ,@IdNAsiento ,@Costo,'999'       ,@IdCID,0       ,         1,@Fecha,'V',getdate())
	
				select 1 as Resultado
			END
		ELSE
			BEGIN
				select 0 as Resultado
			END

 If @@Error<>0
      RollBack Transaction                
 Else
     Commit Transaction