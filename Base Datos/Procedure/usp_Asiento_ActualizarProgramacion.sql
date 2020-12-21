Alter Procedure usp_Asiento_ActualizarProgramacion  
@NroViaje			Int,  
@IdCID				varchar(50),  
@Fecha				Datetime,  
@Codi_Programacion  int  
as  
Set Nocount On  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
Begin Transaction     
 Update Asiento set Codi_programacion=@Codi_Programacion,t_ruta='P',DateTimeUpdated=getdate()
 Where Codi_Programacion=@NroViaje and Fecha=@Fecha and IdCID=@IdCID  
 If @@Error<>0  
     RollBack Transaction                  
 Else  
     Commit Transaction