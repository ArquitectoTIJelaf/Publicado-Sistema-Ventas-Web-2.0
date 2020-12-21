Alter Procedure usp_Reserva_Insert_one
@Id_VentaWeb			Numeric(18), 
@FechaViaje				smalldatetime, 
@HoraViaje				varchar(10),
@OrigenId				smallint, 
@DestinoID				smallint,
@Codi_Servicio			tinyint,
@Dni					Varchar(15),
@Edad					tinyint,
@Nume_Asiento			Varchar(3),
@Codi_Programacion		int,
@Nombres				Varchar(100),
@Costo					Real,
@Nit_Cliente			Varchar(11),
@Cod_tipo				Varchar(2),
@Telefono				Varchar(15),
@Sexo					char(1),
@Cod_Cliente			real,
@Per_Autoriza			char(1),
@RecogeId				smallint, 
@SubeId					smallint,
@BajaId					smallint,
@Hora_Embarque			varchar(7),
@Flag_Venta				varchar(2),
@Vale_Remoto			varchar(1),
@Codi_Empresa			tinyint,
@ComisionWeb			real,
@ProveedorWeb			int,
@FechaReserva			Varchar(10),
@HoraReserva			Varchar(10)
as
SET NOCOUNT ON
BEGIN TRY
		BEGIN TRAN
		declare @IGV	tinyint
		declare @Id_venta	int
		declare @Tipo_Doc	Varchar(2)
		Declare @Reco_Venta	Varchar(100)
		Declare @Serie_Boleto			Varchar(4)
		Declare @Nume_Boleto			int
		
		Set @Serie_Boleto='-98'
		SELECT @Nume_Boleto=Cast(correlativo as Int)+1 from TB_CORRELATIVO_AUXILIARES 
		where tabla='tb_RESERVAS' and oficina=upper('999')


		
		select @Reco_Venta= Descripcion from Tb_Oficinas Where Codi_Sucursal=@SubeId
		SELECT     @Tipo_Doc=COD_TIP FROM         dbo.TABLAS
		WHERE     (COD_TAB = '56') AND (SAB_CON < 8) and SAB_CON=@Cod_tipo

		If Exists(Select Top 1 1 from Venta Where SERIE_BOLETO=@Serie_Boleto and NUME_BOLETO=@Nume_Boleto 
					and CODI_EMPRESA=@Codi_Empresa and tipo='M' AND ISNUMERIC(SERIE_BOLETO)=1)
			Begin
				Select @Nume_Boleto=(Max(NUME_BOLETO)+1) from Venta Where SERIE_BOLETO=@Serie_Boleto and CODI_EMPRESA=@Codi_Empresa and tipo='M' AND ISNUMERIC(SERIE_BOLETO)=1
			End

		SELECT @IGV=cast(Cod_Emp as tinyint) FROM tablas Where Nom_tab='IGV' and Cod_Tip='03'
		insert into venta(
		DNI, EDAD, NUME_ASIENTO, CODI_PROGRAMACION, 
		CODI_SUBRUTA, COD_ORIGEN, NOMBRE, PREC_VENTA, NIT_CLIENTE,
		Serie_Boleto,NUME_BOLETO, CODI_SUCURSAL, CODI_EMPRESA, FLAG_VENTA, 
		FECH_VENTA,  PUNTO_VENTA, TIPO_DOC, TIPO, INDI_ANULADO,
		CODI_CLIENTE, RECO_VENTA, CLAV_USUARIO, FECH_ANULACION, 
		TELEFONO, CODI_ESCA, PER_AUTORIZA, CLAV_USUARIO1,SEXO,ESTADO_ASIENTO,
		tipo_pago,SUC_VENTA, Id_VentaWeb,Vale_Remoto)
		Values(
		@Dni, @Edad,@Nume_Asiento, @Codi_Programacion,@DestinoId, 
		@OrigenId, upper(@Nombres), @Costo, 
		@Nit_Cliente,@Serie_Boleto,@Nume_Boleto, '999', @Codi_Empresa, @Flag_Venta,convert(varchar(10),getdate(),103),'999',@Tipo_Doc, 'M',
		'F', @Cod_Cliente, @Reco_Venta, '999', '1900-01-01 00:00:00.000',@Telefono , '',  @Per_Autoriza, '',@Sexo,'N',
		'01','999',@Id_VentaWeb,@Vale_Remoto )

		set @Id_venta=0
		Select @Id_venta=id_Venta from Venta Where SERIE_BOLETO=@Serie_Boleto and CODI_EMPRESA=@Codi_Empresa and tipo='M' and nume_boleto=@Nume_Boleto
		
		if(isnull(@Id_venta,0)<1)
			Begin	
				set @Id_venta=scope_identity()			
			End
		--set @Id_venta=scope_identity()			
							
		INSERT INTO VENTA_DERIVADA(
			Id_venta,
			Nume_Boleto,
			Fecha_Viaje,
			Hora_Viaje,
			SErvicio,
			Porcentaje,
			Tota_Ruta1,
			Tota_Ruta2,
			Nacionalidad,
			Recoje_En,
			Sube_En,
			Hora_Embarque_Web,
			baja_en, 
			Comision_web,
			proveedor_tar
		)
		values( 
			@Id_venta,
			('-98' + (right('0000000'+cast(@Nume_Boleto as Varchar(10)),7))),
			@FechaViaje,
			@Hora_Embarque,--@HoraViaje,
			@Codi_Servicio,
			@IGV,
			((@Costo*@IGV)/100),
			((@Costo*(100-@IGV))/100),
			'',
			@RecogeId,
			@SubeId,
			@Hora_Embarque,
			@BajaId, 
			@ComisionWeb,
			@ProveedorWeb 
		)

		Insert Into tb_reservacion_hora_fecha(ID_VENTA,NUME_BOLETO,FECHA_C,HORA_C)
		Values (@Id_Venta,('-98' + (right('0000000'+cast(@Nume_Boleto as Varchar(10)),7))),@FechaReserva,@HoraReserva)

		update tb_Correlativo_Auxiliares Set Correlativo=cast(@Nume_Boleto as Varchar(15))
		Where Tabla='tb_RESERVAS' 

		select isnull(@Id_venta,0) as id_venta

		COMMIT TRAN 

END TRY
BEGIN CATCH
	ROLLBACK TRAN 
	
	INSERT INTO TB_HISTORICO_ERROR (Modulo,tipo,serie,numero,errNumber,errSeverity,errState,errProcedure,errLine,errMessage)
	SELECT 'B','M',@Serie_Boleto,@nume_boleto,ERROR_NUMBER() AS errNumber,ERROR_SEVERITY() AS errSeverity,ERROR_STATE() AS errState
	,ERROR_PROCEDURE() AS errProcedure,ERROR_LINE() AS errLine,ERROR_MESSAGE() AS errMessage
	
	select isnull(@Id_venta,0) as id_venta

END CATCH