GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_VentaReserva_Insert_FE')
	BEGIN
		DROP PROCEDURE usp_VentaReserva_Insert_FE
	End
GO
GO
Create Procedure usp_VentaReserva_Insert_FE
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
@Tipo_Doc				Varchar(2),
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
@Id_Venta				Int Output
as
SET NOCOUNT ON
BEGIN TRY
		BEGIN TRAN

		declare @IGV					tinyint
		declare @Tipo					Varchar(2)
		Declare @Numero					Int
		Declare @Reco_Venta				Varchar(150)
		declare @POS					tinyint
		
		--Obtener Embarque
		Select @Reco_Venta=Descripcion From Tb_PuntoVenta
		Where Codi_puntoVenta=@SubeId
		SET @POS=0;
		----Hallar Tipo Documento Venta----
		Set @Tipo='B'
		--Set @Tipo_Documento='03'
		if RTRIM(LTRIM(@Nit_Cliente))<>''
			Begin
				Set @Tipo='F'
			End
		
		--Generar Correlativo
		Declare @Codi_Documento	Varchar(2)
		Declare @Serie_Documento	Varchar(5)
		Declare @Numero_Documento	Varchar(10)
		Set @Codi_Documento='03'
		If LTRIM(RTRIM(@Nit_Cliente))<>''
			Begin
				Set @Codi_Documento='01'
			End
		Select @Serie_Documento=Serie,@Numero_Documento=Numero from Tb_Correlativo_Documento
		Where Codi_Empresa=@Codi_Empresa and Codi_Documento=@Codi_Documento 
		and Codi_Sucursal='999' and Codi_PuntoVenta='999' and Terminal='999' and Tipo='E'
		SET @POS=1;
		
		---numerico int --alfa varchar
		Declare @Serie_Boleto			VARCHAR(4)

		Declare @Nume_Boleto			int

		---numerico int --alfa varcharSET @Serie_Boleto=CAST(@Serie_Boleto AS Int)
		SET @Serie_Boleto=@Serie_Documento
		SET @Nume_Boleto=CAST(@Numero_Documento AS Int)

		If Exists(Select Top 1 1 from Venta Where SERIE_BOLETO=@Serie_Boleto and NUME_BOLETO=@Nume_Boleto 
					and CODI_EMPRESA=@Codi_Empresa and tipo=@Tipo AND ISNUMERIC(SERIE_BOLETO)=1)
			Begin
				Select @Nume_Boleto=(Max(NUME_BOLETO)+1) from Venta 
				Where SERIE_BOLETO=@Serie_Boleto and CODI_EMPRESA=@Codi_Empresa and tipo=@Tipo AND ISNUMERIC(SERIE_BOLETO)=1
			End


		SELECT @IGV=cast(Cod_Emp as tinyint) FROM tablas Where Nom_tab='IGV' and Cod_Tip='03'
		Set @Per_Autoriza='1'
		insert into venta(
		DNI, EDAD, NUME_ASIENTO, CODI_PROGRAMACION, 
		CODI_SUBRUTA, COD_ORIGEN, NOMBRE, PREC_VENTA, NIT_CLIENTE,
		Serie_Boleto,NUME_BOLETO, CODI_SUCURSAL, CODI_EMPRESA, FLAG_VENTA, 
		FECH_VENTA,  PUNTO_VENTA, TIPO_DOC, TIPO, INDI_ANULADO,
		CODI_CLIENTE, RECO_VENTA, CLAV_USUARIO, FECH_ANULACION, 
		TELEFONO, CODI_ESCA, PER_AUTORIZA, CLAV_USUARIO1,SEXO,ESTADO_ASIENTO,
		tipo_pago,SUC_VENTA, Id_VentaWeb,Vale_Remoto,idmodulo)
		Values(
		@Dni, @Edad,@Nume_Asiento, @Codi_Programacion,@DestinoId, 
		@OrigenId, upper(@Nombres), @Costo, 
		@Nit_Cliente,@Serie_Boleto,@Nume_Boleto, '999', @Codi_Empresa, @Flag_Venta,convert(varchar(10),getdate(),103),'999',@Tipo_Doc, @Tipo,
		'F', @Cod_Cliente, @Reco_Venta, '999', '01/01/1900',@Telefono , '',  @Per_Autoriza, '',@Sexo,'N',
		'01','999',@Id_VentaWeb,@Vale_Remoto,5)
		
		SET @POS=2;
		Select @Id_venta=id_Venta
		from Venta 
		Where SERIE_BOLETO=@Serie_Boleto and 
		CODI_EMPRESA=@Codi_Empresa and 
		tipo=@Tipo and 
		nume_boleto=@Nume_Boleto
								
		SET @POS=3;
		INSERT INTO VENTA_DERIVADA(Id_venta,Nume_Boleto,Fecha_Viaje,Hora_Viaje,SErvicio,Porcentaje,Tota_Ruta1,Tota_Ruta2,Nacionalidad,Recoje_En,Sube_En,Baja_En,Hora_Embarque_Web,Comision_web,proveedor_tar)
		values( @Id_venta,(Right(('00'+@Serie_Boleto),3) + '-' + Right(('000000'+Cast(@Nume_Boleto as Varchar(6))),7)),@FechaViaje,@HoraViaje,@Codi_Servicio,
		@IGV,((@Costo*@IGV)/100),((@Costo*(100-@IGV))/100),'',@RecogeId,@SubeId,@BajaId,@Hora_Embarque,@ComisionWeb,@ProveedorWeb )


		SET @POS=4;
		Update Tb_Correlativo_Documento
		Set Numero=Right(('000000'+Cast((@Nume_Boleto+1)as Varchar)),7)
		Where Codi_Empresa=@Codi_Empresa and Codi_Documento=@Codi_Documento 
		and Codi_Sucursal='999' and Codi_PuntoVenta='999' and Terminal='999' and Tipo='E'
		and Serie=@Serie_Documento
		SET @POS=5;
		Declare @Table Table(Error Varchar(1),Cabecera Varchar(4000),Detalle Varchar(4000),datosAdicionales Varchar(4000),Receptor Varchar(4000))
		Insert Into @Table(Error,Cabecera,datosAdicionales,Detalle,Receptor)
		Exec Usp_Fac_Elec_Venta_Web @Id_venta
		SET @POS=6;
		select Error,Cabecera,datosAdicionales,Detalle,Receptor,@Nume_Boleto Numero_Documento ,@Id_venta Id_Venta,@Serie_Boleto Serie_Documento from @Table
	
		COMMIT TRAN 

END TRY
BEGIN CATCH

	ROLLBACK TRAN
	
	INSERT INTO TB_HISTORICO_ERROR (Modulo,tipo,serie,numero,errNumber,errSeverity,errState,errProcedure,errLine,errMessage,POSICION)
	SELECT 'W',@tipo,@Serie_Boleto,@nume_boleto,ERROR_NUMBER() AS errNumber,ERROR_SEVERITY() AS errSeverity,ERROR_STATE() AS errState
	,ERROR_PROCEDURE() AS errProcedure,ERROR_LINE() AS errLine,ERROR_MESSAGE() AS errMessage,@POS
	
	select NULL Error,NULL Cabecera,NULL datosAdicionales,NULL Detalle,NULL Receptor,0 Numero_Documento ,0 Id_Venta,0 Serie_Documento from @Table
END CATCH

