GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_VentaReserva_Insert_one')
	BEGIN
		DROP PROCEDURE usp_VentaReserva_Insert_one
	End
GO
GO
Create Procedure usp_VentaReserva_Insert_one
@Id_VentaWeb			Numeric(18), 
@FechaViaje				smalldatetime, 
@HoraViaje				varchar(10),
@OrigenId				smallint, 
@DestinoID				smallint,
@Nume_Boleto			int,
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
@ProveedorWeb			int
as
Begin Transaction 
declare @IGV	tinyint
declare @Id_venta	int
Declare @Reco_Venta	Varchar(100)

select @Reco_Venta= Descripcion from Tb_Oficinas Where Codi_Sucursal=@SubeId

SELECT @IGV=cast(Cod_Emp as tinyint) FROM tablas Where Nom_tab='IGV' and Cod_Tip='03'
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
		@Nit_Cliente,0,@Nume_Boleto, '999', @Codi_Empresa, @Flag_Venta,convert(varchar(10),getdate(),103),'999',@Tipo_Doc, 'A',
		'F', @Cod_Cliente, @Reco_Venta, '999', '1900-01-01 00:00:00.000',@Telefono , '',  @Per_Autoriza, '',@Sexo,'N',
		'01','999',@Id_VentaWeb,@Vale_Remoto,5 )

		set @Id_venta=scope_identity()			
							
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
			('000-' + (right('0000000'+cast(@Nume_Boleto as Varchar(10)),7))),
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

		--Grabar Comida
		Insert Into tb_comidaxboleto(Id_venta,Codi_ComidaServicio)
		Values(@Id_venta,'00')
	--update WebOrders set Estado_WebOrders=10 where Id_WebOrders=@IdWebOrder
		
		update tb_Correlativo_Auxiliares Set Correlativo=cast(@Nume_Boleto as Varchar(15))
		Where Tabla='Venta_Correlativo' 
		select @Id_venta as id_venta
 If @@Error<>0
	      Begin
	        	RollBack Transaction                
	      End 
    Else
	     Begin
	       	Commit Transaction  
	     End
