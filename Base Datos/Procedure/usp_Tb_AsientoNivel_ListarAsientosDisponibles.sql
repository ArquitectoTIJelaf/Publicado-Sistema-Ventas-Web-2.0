Alter Procedure usp_Tb_AsientoNivel_ListarAsientosDisponibles 
@Codi_Programacion		int,
@Codi_bus				varchar(5),
@Codi_Plano				Varchar(4)

as

Set NoCount On

	Declare @Codi_Sucursal			SmallInt
	Declare @Codi_Ruta				SmallInt
	Declare @Codi_Servicio			TinyInt
	Declare @Codi_Empresa			TinyInt
	Declare @Turno					Varchar(10)


	Select distinct @Codi_Empresa= mp.Codi_Empresa,@Codi_Sucursal=rm.Codi_Sucursal,
	@Codi_Ruta=rm.CODI_DESTINO,@Codi_Servicio=rm.Codi_Servicio,@Turno=mp.HORA
	From Tb_Maestro_Programacion mp
	Inner Join Tb_Ruta_Maestro rm on mp.NRO_RUTA=rm.NRO_RUTA
	Where mp.NRO_VIAJE in(
		Select Top 1 NRO_VIAJE From Tb_Viaje_Programacion Where Codi_programacion=@Codi_Programacion
	)
	Declare @Asientos				Varchar(100)
	Declare @Codi_OrigenP			SmallInt
	Declare @Codi_DestinoP			SmallInt

	Declare @NumeroBloque			Int
	Declare @CantBloques			Int
	Declare @Tabla					Table (Nume_Asiento SmallInt)

	DECLARE AsientoBloqInfo CURSOR FOR 
				Select
				Asientos From Tb_AsientosBloqueados 
				Where Cod_OrigenB=@Codi_Sucursal and Cod_DestinoB=@Codi_Ruta 
				and Cod_Servicio=@Codi_Servicio and cod_empresa=@Codi_Empresa
				and horario=@Turno
	OPEN AsientoBloqInfo
	FETCH NEXT FROM AsientoBloqInfo INTO @Asientos
	WHILE @@fetch_status = 0
	BEGIN
		Set @CantBloques=Len(@Asientos)
		Set @NumeroBloque=1
		While (@NumeroBloque<@CantBloques)
			Begin
				Insert Into @Tabla(Nume_Asiento)
				Values(cast(SUBSTRING(@Asientos,@NumeroBloque,2)as Int))
				Set @NumeroBloque=@NumeroBloque+2
			End
		FETCH NEXT FROM AsientoBloqInfo INTO @Asientos
	END
	CLOSE AsientoBloqInfo
	DEALLOCATE AsientoBloqInfo


		Select  cast(Nume_Asiento as smallint)as n_asiento,Nivel from tb_asientonivel
		Where (Not (Nume_ASiento in(
		select Nume_Asiento from venta Where Codi_programacion=@Codi_Programacion and Indi_Anulado='F')))
		and (Not (Nume_ASiento in(
		select Nume_Asiento from asiento Where Codi_programacion=@Codi_Programacion)))
		and (Not (Nume_ASiento in(
		select Nume_Asiento from @Tabla)))
		and  (Nume_ASiento in(
		select tipo from Tb_planobus Where alto<>0 and Tipo<>'EE' and Tipo<>'BA' and Tipo<>'VA'  and Tipo<>'CU' and Tipo<>'PU' and Tipo<>'TV' and Tipo<>'LI' and Codigo=@Codi_Plano))
		and Codi_Bus=@Codi_bus and Estado=1
		Order by Nume_ASiento asc