ALter PROCEDURE usp_Asiento_ValidarReservarByProgramacion
@NroViaje 		Int,
@IdNAsiento		varchar(2)
as
Set NoCount On

SELECT CODI_PROGRAMACION FROM Asiento where CODI_PROGRAMACION=@NroViaje AND NUME_ASIENTO=@IdNAsiento 
Union
Select Codi_Programacion From Venta Where CODI_PROGRAMACION=@NroViaje and NUME_ASIENTO=@IdNAsiento
AND INDI_ANULADO='F'
