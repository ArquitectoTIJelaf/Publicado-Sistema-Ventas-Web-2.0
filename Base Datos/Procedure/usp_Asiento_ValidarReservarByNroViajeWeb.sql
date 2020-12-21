Alter PROCEDURE usp_Asiento_ValidarReservarByNroViajeWeb
@NroViaje 		Int,
@IdNAsiento		varchar(2),
@Fecha			Datetime
as
Set NoCount On

SELECT CODI_PROGRAMACION FROM Asiento 
where CODI_PROGRAMACION=@NroViaje AND NUME_ASIENTO=@IdNAsiento and Fecha=@Fecha