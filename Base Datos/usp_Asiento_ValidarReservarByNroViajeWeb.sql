GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Asiento_ValidarReservarByNroViajeWeb')
	BEGIN
		DROP PROCEDURE usp_Asiento_ValidarReservarByNroViajeWeb
	End
GO
GO
Create PROCEDURE usp_Asiento_ValidarReservarByNroViajeWeb
@NroViaje 		Int,
@IdNAsiento		varchar(2),
@Fecha			Datetime
as
Set NoCount On

SELECT CODI_PROGRAMACION FROM Asiento 
where CODI_PROGRAMACION=@NroViaje AND NUME_ASIENTO=@IdNAsiento and Fecha=@Fecha