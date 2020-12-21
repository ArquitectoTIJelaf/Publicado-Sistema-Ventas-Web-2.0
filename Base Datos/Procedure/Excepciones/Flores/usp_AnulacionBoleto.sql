Create Procedure usp_AnulacionBoleto
@Serie_Boleto		Int,
@Nume_Boleto		Int,
@Codi_Empresa		TinyInt
as
	BEGIN
		BEGIN TRANSACTION
			UPDATE VENTA
			SET 
				INDI_ANULADO='T',
				FECH_ANULACION=Convert(Varchar(10),getdate(),103),
				clav_usuario1=999
			Where	SERIE_BOLETO=Cast(@Serie_Boleto as Varchar(5))
			and		NUME_BOLETO=@Nume_Boleto
			and		CODI_EMPRESA=@Codi_Empresa
		IF @@ERROR<>0
			ROLLBACK TRANSACTION
		ELSE
			COMMIT TRANSACTION
	END


