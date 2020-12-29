GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Asiento_ValidarReservarByProgramacion')
	BEGIN
		DROP PROCEDURE usp_Asiento_ValidarReservarByProgramacion
	End
GO
GO
Create PROCEDURE usp_Asiento_ValidarReservarByProgramacion
@NroViaje 		Int,
@IdNAsiento		varchar(2)
as
Set NoCount On

SELECT CODI_PROGRAMACION FROM Asiento where CODI_PROGRAMACION=@NroViaje AND NUME_ASIENTO=@IdNAsiento 
Union
Select Codi_Programacion From Venta Where CODI_PROGRAMACION=@NroViaje and NUME_ASIENTO=@IdNAsiento
AND INDI_ANULADO='F'
