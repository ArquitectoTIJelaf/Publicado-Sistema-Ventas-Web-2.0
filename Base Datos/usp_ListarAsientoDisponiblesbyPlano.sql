GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_ListarAsientoDisponiblesbyPlano')
	BEGIN
		DROP PROCEDURE usp_ListarAsientoDisponiblesbyPlano
	End
GO
GO
Create Procedure usp_ListarAsientoDisponiblesbyPlano
@Codi_bus		varchar(5),
@Fecha			smalldatetime,
@Nro_Viaje		Int,
@Codi_Plano		VarChar(4)
as
Set NoCount On
		Select  cast(Nume_Asiento as smallint)as n_asiento,Nivel from tb_asientonivel
		Where (Not (Nume_Asiento in(
		select Nume_Asiento from asiento Where Codi_programacion=@Nro_Viaje and Fecha=@Fecha)))
		and  (Nume_Asiento in(
		select tipo from Tb_planobus Where alto<>0 and Tipo<>'EE' and Tipo<>'BA' and Tipo<>'VA'  and Tipo<>'CU' and Tipo<>'PU' and Tipo<>'TV' and Tipo<>'LI' and Codigo=@Codi_Plano))
		and Codi_Bus=@Codi_bus and Estado=1
		Order by Nume_Asiento asc

