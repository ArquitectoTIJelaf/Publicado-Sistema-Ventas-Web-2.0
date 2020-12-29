GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Venta_Insert_FE')
	BEGIN
		DROP PROCEDURE usp_Venta_Insert_FE
	End
GO
GO
Create Procedure usp_Venta_Insert_FE
@Id_VentaWeb			Numeric(18), 
@FechaViaje				smalldatetime, 
@HoraViaje				varchar(10),
@OrigenId				smallint, 
@DestinoID				smallint,
@Id_Users				Numeric(18) ,
@IdWebOrder				Numeric(18),
@Serie_Boleto			int,
@Nume_Boleto			int,
@Tipo_Documento			Varchar(3),
@Codi_Servicio			tinyint,
@IDCID					varchar(50),
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
@ProveedorWeb			int
as
	Begin Transaction 

		declare @IGV					tinyint
		declare @Id_venta				int
		declare @Tipo_Doc				Varchar(2)
		declare @Tipo					Varchar(2)
		Declare @Numero					Int
		Declare @Reco_Venta				Varchar(150)
		SELECT     @Tipo_Doc=COD_TIP FROM         dbo.TABLAS
		WHERE     (COD_TAB = '56') AND (SAB_CON < 8) and SAB_CON=@Cod_tipo

		--Obtener Embarque
		Select @Reco_Venta=Descripcion From Tb_PuntoVenta
		Where Codi_puntoVenta=@SubeId

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

		SET @Serie_Boleto=CAST(@Serie_Documento AS Int)
		SET @Nume_Boleto=CAST(@Numero_Documento AS Int)


		SELECT @IGV=cast(Cod_Emp as tinyint) FROM tablas Where Nom_tab='IGV' and Cod_Tip='03'
		Set @Per_Autoriza='1'
		insert into venta(
		DNI, EDAD, NUME_ASIENTO, CODI_PROGRAMACION, 
		CODI_SUBRUTA, COD_ORIGEN, NOMBRE, PREC_VENTA, NIT_CLIENTE,
		Serie_Boleto,NUME_BOLETO, CODI_SUCURSAL, CODI_EMPRESA, FLAG_VENTA, 
		FECH_VENTA,  PUNTO_VENTA, TIPO_DOC, TIPO, INDI_ANULADO,
		CODI_CLIENTE, RECO_VENTA, CLAV_USUARIO, FECH_ANULACION, 
		TELEFONO, CODI_ESCA, PER_AUTORIZA, CLAV_USUARIO1,SEXO,ESTADO_ASIENTO,
		tipo_pago,SUC_VENTA, Id_VentaWeb,Vale_Remoto,idModulo)
		Values(
		@Dni, @Edad,@Nume_Asiento, @Codi_Programacion,@DestinoId, 
		@OrigenId, upper(@Nombres), @Costo, 
		@Nit_Cliente,(@Tipo + Cast(@Serie_Boleto as Varchar(3))),@Nume_Boleto, '999', @Codi_Empresa, @Flag_Venta,convert(varchar(10),getdate(),103),'999',@Tipo_Doc, @Tipo,
		'F', @Cod_Cliente, @Reco_Venta, '999', '1900-01-01 00:00:00.000',@Telefono , '',  @Per_Autoriza, '',@Sexo,'N',
		'01','999',@Id_VentaWeb,@Vale_Remoto,5 )

		set @Id_venta=scope_identity()								

		INSERT INTO VENTA_DERIVADA(Id_venta,Nume_Boleto,Fecha_Viaje,Hora_Viaje,SErvicio,Porcentaje,Tota_Ruta1,Tota_Ruta2,Nacionalidad,Recoje_En,Sube_En,Baja_En,Hora_Embarque_Web,Comision_web,proveedor_tar)
		values( @Id_venta,((@Tipo + Cast(@Serie_Boleto as Varchar(3))) + '-' + Right(('000000'+Cast(@Nume_Boleto as Varchar(6))),7)),@FechaViaje,@HoraViaje,@Codi_Servicio,
		@IGV,((@Costo*@IGV)/100),((@Costo*(100-@IGV))/100),'',@RecogeId,@SubeId,@BajaId,@Hora_Embarque,@ComisionWeb,@ProveedorWeb )


		Update Tb_Correlativo_Documento
		Set Numero=Right(('000000'+Cast((@Nume_Boleto+1)as Varchar)),7)
		Where Codi_Empresa=@Codi_Empresa and Codi_Documento=@Codi_Documento 
		and Codi_Sucursal='999' and Codi_PuntoVenta='999' and Terminal='999' and Tipo='E'
		and Serie=@Serie_Documento

		Set @Numero=@Nume_Boleto
		select @Numero as Numero

	If @@Error<>0
		Begin
	    	RollBack Transaction
		End
	Else
		Begin
	    	Commit Transaction
		End