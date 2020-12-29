GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Asiento_ReservarByProgramacion')
	BEGIN
		DROP PROCEDURE usp_Asiento_ReservarByProgramacion
	End
GO
GO
Create PROCEDURE usp_Asiento_ReservarByProgramacion 
@Codi_Programacion 		int,
@IdNAsiento				varchar(2),
@IdCID					varchar(50),
@Costo					Numeric(9,2),
@Fecha					smalldatetime

as
  Begin Transaction      

			IF NOT EXISTS(
				SELECT TOP 1 1 FROM Asiento where CODI_PROGRAMACION=@Codi_Programacion AND NUME_ASIENTO=@IdNAsiento 
				Union
				Select Top 1 1 From Venta Where CODI_PROGRAMACION=@Codi_Programacion and NUME_ASIENTO=@IdNAsiento
				AND INDI_ANULADO='F'
			)
				BEGIN
					insert into asiento  (CODI_PROGRAMACION,NUME_ASIENTO,Costo,CODI_TERMINAL,IdCID,Id_Users, TypeApp_Id,t_Ruta,Fecha) 
					values(@Codi_Programacion,@IdNAsiento,@Costo,'999',@IdCID,0,1,'P',@Fecha)
					Declare		@IDS			Numeric(18)	
					Set @IDS=scope_identity()
		
					select '1' as Resultado
				END
			ELSE
				BEGIN
					select '0' as Resultado
				END

 If @@Error<>0
        RollBack Transaction                
 Else
     Commit Transaction
