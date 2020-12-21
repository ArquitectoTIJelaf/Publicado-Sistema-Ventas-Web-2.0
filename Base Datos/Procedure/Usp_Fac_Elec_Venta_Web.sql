Create procedure Usp_Fac_Elec_Venta_Web
@id_venta_Ent int
as
SET NOCOUNT ON
Begin
	BEGIN TRY
		--------------------------------------------------------------------------------------------
		--------------------------------------------------------------------------------------------
		--------------------------------------------------------------------------------------------
		declare @Cabecera varchar(max),@datosAdicionales VARCHAR(MAX),@Detalle varchar(max)
		declare @CODI_DOCUMENTO varchar(2),@tipo varchar(1),@serie varchar(4),@numero varchar(8),@fecha datetime,@total numeric(18,2),@origen varchar(100),@destino varchar(100),@ori_pas int=0,
		@pv varchar(100),@usu varchar(100),@id_venta int,@tike varchar(50),@doc_cli varchar(50),@nom_cli varchar(150),@tel_cli varchar(50),@Suc varchar(100),@prog int,@raz varchar(200)
		,@ori_prog varchar(100),@des_prog varchar(100),@ser_prog varchar(100),@asiento varchar(2),@fec_prog  datetime,@hor_prog varchar(20),@ruc varchar(20),@tipo_doc_cli varchar(8),@des_pas int=0
		,@HORA_EMI VARCHAR(16),@Receptor varchar(max),@dir_ruc varchar(200),@FLG_VTA VARCHAR(2),@PAGO VARCHAR(2),@codi_Empresa INT,@BUS VARCHAR(6),@Poliza_Nro VARCHAR(200)='',@TDOC_C VARCHAR(2)
		,@TDOC_C_SUNAT VARCHAR(1),@PUNTO_VENTA INT,@codigo_usuario int,@HORA_PASO VARCHAR(11)='',@FECHA_PASO DATETIME,@CODI_SUCURS INT,@DIA INT,@FECHA_POL DATE='01/01/1900'
		Declare @Sube int=0 , @Baja int=0,@FECHA_VEN DATE='01/01/1900',@igv real=0.00

		

		SET @CODI_DOCUMENTO='' SET @tipo='' SET @serie='' SET @numero='' SET @fecha='01/01/1900' SET @total=0 SET @origen='' SET @destino='' SET @pv='' SET @Receptor=''
		SET @usu='' SET @id_venta=0 SET @tike='' SET @doc_cli='' SET @nom_cli='' SET @tel_cli='' SET @Suc='' SET @prog='' SET @raz='' SET @ori_prog='' SET @dir_ruc=''
		SET @des_prog='' SET @ser_prog='' SET @asiento='' SET @fec_prog='01/01/1900' SET @hor_prog='' SET @ruc='' SET @tipo_doc_cli='' SET @HORA_EMI='' SET @FLG_VTA=''
		SET @BUS='' SET @Poliza_Nro='' set @TDOC_C_SUNAT='' set @TDOC_C=''

		select @CODI_SUCURS=V.CODI_SUCURSAL ,@tipo=tipo,@serie=SERIE_BOLETO,@numero=NUME_BOLETO,@fecha=FECH_VENTA,@total=PREC_VENTA,@origen=ori.descripcion,@destino=de.Descripcion,@PUNTO_VENTA=V.Punto_Venta,@pv=pv.Descripcion,@usu=us.Login,
		@id_venta= v.id_venta,@doc_cli=v.dni,@nom_cli=v.nombre,@tel_cli=TELEFONO,@Suc=suc.Descripcion,@prog=CODI_PROGRAMACION,@asiento=NUME_ASIENTO,@ruc=NIT_CLIENTE,@tipo_doc_cli=TIPO_DOC
		,@FLG_VTA=V.FLAG_VENTA,@codi_Empresa=V.Codi_Empresa ,@TDOC_C=V.TIPO_DOC ,@codigo_usuario=v.CLAV_USUARIO,@ori_pas=v.cod_origen, @des_pas =v.CODI_SUBRUTA
		from venta v inner join Tb_Oficinas ori on ori.Codi_Sucursal=v.cod_origen  INNER JOIN Tb_Oficinas pv on v.Punto_Venta=pv.Codi_Sucursal inner join tb_usuario us on 
		us.Codi_Usuario = v.CLAV_USUARIO inner join Tb_Oficinas suc on suc.Codi_Sucursal=v.Codi_Sucursal
		inner join Tb_Oficinas de on de.Codi_Sucursal=v.CODI_SUBRUTA where id_venta=@id_venta_Ent
	
		if @tipo='F' begin set @CODI_DOCUMENTO='01' end	else begin set @CODI_DOCUMENTO='03' end

		PRINT @fecha
		select top 1 @igv=igv/100.00 from Tb_IgvHistorio where fecha<=@fecha order by fecha desc
		PRINT @igv
		select @ori_prog=o.descripcion,@des_prog=d.Descripcion,@ser_prog= s.descripcion,@fec_prog=Fech_programacion,@hor_prog=Hora_programacion,@BUS=Codi_Bus 
		from tb_programacion p inner join tb_oficinas o on  p.codi_sucursal=o.codi_sucursal inner join tb_oficinas d on d.codi_sucursal=p.codi_ruta inner join tb_servicio s
		on s.codi_servicio= p.codi_servicio where Codi_Programacion=@prog

		SET @HORA_EMI=convert(varchar,GetDate(),108)

		IF @FLG_VTA='1' BEGIN SET @PAGO='02' END ELSE BEGIN SET @PAGO='01' END
		
		set @Cabecera=@CODI_DOCUMENTO +'|'+ @tipo + right('000'+ltrim(@serie),3)  +'|'+ right('00000000'+ltrim(@numero),8)  +'|' + CONVERT(VARCHAR(10), @fecha, 103)
		+'|' + @HORA_EMI +'|PEN|' + LTRIM(@total) +'|0||0|'+
		CASE WHEN @igv <> 0	THEN ltrim(@total/(1.0+@igv))ELSE '0'END+'|' + 
		CASE WHEN @igv <> 0	THEN '0' ELSE LTRIM(@total) END +'|0|'+	
		CASE WHEN @igv <> 0 THEN ltrim(@igv) ELSE '0' END +'|'+
		CASE WHEN @igv <> 0	THEN ltrim(@total-(@total/(1.0+@igv))) ELSE '0'END 
		+'|0.00|0.00|0.00|0.00|0.00|0.00||||||||' + REPLACE(@origen,'|','') + '|' + REPLACE(@destino ,'|','') + 
		'|0.00|01/01/1900|0.00|0.00|' + dbo.CantidadConLetra(@total)


		--set @Cabecera=@CODI_DOCUMENTO +'|'+ @tipo + right('000'+ltrim(@serie),3)  +'|'+ right('00000000'+ltrim(@numero),8)  +'|' + CONVERT(VARCHAR(10), @fecha, 103)
		--+'|' + @HORA_EMI +'|PEN|' + LTRIM(@total) +'|0||0|0|' + LTRIM(@total) +'|0|0|0'
		--+'|0.00|0.00|0.00|0.00|0.00|0.00||||||||' + REPLACE(@origen,'|','') + '|' + REPLACE(@destino ,'|','') + '|0.00|01/01/1900|0.00|0.00|' + dbo.CantidadConLetra(@total)
		
		SELECT @ser_prog=S.descripcion,@Sube=isnull(Sube_En,@ori_pas),@Baja=isnull(baja_en,@des_pas) FROM VENTA_DERIVADA V JOIN Tb_Servicio S ON V.Servicio =S.Codi_Servicio WHERE id_venta =@id_venta
		SELECT @Poliza_Nro=Nro_Poliza,@FECHA_POL=Fecha_Reg,@FECHA_VEN=Fecha_Ven  FROM Tb_Bus_Poliza WHERE codi_Bus=@BUS and codi_Empresa=@codi_Empresa and fecha_reg<=@fecha 
		and Fecha_Ven>=@fecha and estado='1'
		
		DECLARE @PILO VARCHAR(500)=''
		if @FECHA_POL='01/01/1900'
		begin
			SET @PILO =''
		end
		else
		begin
			SET @PILO ='Cubierto por ' + REPLACE(@Poliza_Nro ,'|','') + ' '  + CONVERT(varchar, @FECHA_POL , 103) + '-' + CONVERT(varchar, @FECHA_VEN , 103)
		end
		DECLARE @DIR_PV AS VARCHAR(250)='',@tel1 AS VARCHAR(250)='',@tel2 AS VARCHAR(250)='',@embarque varchar(250)='',@desembarque varchar(250)=''
		set @DIR_PV='' set @tel1='' set @tel2=''
		
		SELECT @DIR_PV=REPLACE(direccion ,'|',''),@tel1=REPLACE(telefono1 ,'|',''),@tel2=REPLACE(telefono2  ,'|','')FROM Tb_Direccion_Empresa 
			WHERE codi_empresa=@codi_Empresa AND codi_sucursal=@PUNTO_VENTA

		SELECT @embarque=REPLACE(direccion ,'|','')FROM Tb_Direccion_Empresa WHERE codi_empresa=@codi_Empresa AND codi_sucursal=@Sube
		SELECT @desembarque=REPLACE(direccion ,'|','')FROM Tb_Direccion_Empresa WHERE codi_empresa=@codi_Empresa AND codi_sucursal=@Baja

		--******************** CUALQUIER CLIENTE ******************************
		SET @datosAdicionales= '5|' + LTRIM(@id_venta) +'^10|' + REPLACE(@tike,'^','') + 
		'^50|' + @embarque + '^51|' + @desembarque + '^21|'+ REPLACE(@PILO,'^','') + '^40|'+ REPLACE(@tel1,'^','') +'/'+REPLACE(@tel2,'^','')

		--******************** CUALQUIER FLORES  ******************************
		--SET @datosAdicionales= '1|'+@PAGO+'^2|' + REPLACE(@DIR_PV,'^','') +'^4|' + ltrim(@codigo_usuario)  +'^5|' + LTRIM(@id_venta) +'^10|' + REPLACE(@tike,'^','') + '^16|' 
		--+ REPLACE(@Suc,'^','') +'{} {}'+ '^21|'+ REPLACE(@Poliza_Nro,'^','') + '^40|'+ REPLACE(@tel1,'^','') +'/'+REPLACE(@tel2,'^','')
		--************************************************************************************************

		IF @prog=0	
		BEGIN
			set @Detalle = '1|  |ZZ |1|POR EL SERVICIO DE TRANSPORTE DE LA RUTA ' + @origen  + '-' +  @destino + '/ SERVICIO: ' + @ser_prog  +'/ NRO ASIENTO:' + ''
			+ '/ PASAJERO: ' + @nom_cli  + ' / DNI: ' + @doc_cli + ' / FECHA VIAJE:/ HORA VIAJE:/ PRECIO: ' + LTRIM(@total) + '|' + 
			CASE WHEN @igv <> 0	THEN ltrim(@total/(1.0+@igv)) ELSE LTRIM(@total) END + '|' + 
			LTRIM(@total)  + '|'+
			CASE WHEN @igv <> 0	THEN '10' ELSE '30' END+'|'+ 
			CASE WHEN @igv <> 0	THEN ltrim(@total-(@total/(1.0+@igv))) ELSE '0' END+
			'||0.00|0.00|' + 
			CASE WHEN @igv <> 0	THEN ltrim(@total/(1.0+@igv)) ELSE LTRIM(@total) END
			+ '|0.00|0.00' 
			
			--set @Detalle = '1|  |ZZ |1|POR EL SERVICIO DE TRANSPORTE DE LA RUTA ' + @origen  + '-' +  @destino + '/ SERVICIO: ' + @ser_prog  +'/ NRO ASIENTO:' + ''
			--+ '/ PASAJERO: ' + @nom_cli  + ' / DNI: ' + @doc_cli + ' / FECHA VIAJE:/ HORA VIAJE:/1/' + LTRIM(@total) + '|' + LTRIM(@total) + '|' + LTRIM(@total) + '|10|' 
			--+ '0||0.00|0.00|' + LTRIM(@total) + '|0.00|0.00' ---ltrim(@total-cast(@total/1.18 as numeric(18,2))) +
		END
		ELSE
		BEGIN
			SELECT @HORA_PASO = ISNULL(HORA_PASO,''),@DIA=DIAS FROM Tb_Viaje_Programacion V JOIN Tb_Ruta_Intermedio R ON 
			V.NRO_VIAJE=R.NRO_VIAJE AND R.CODI_SUCURSAL=@CODI_SUCURS WHERE Codi_programacion=@prog
			IF @HORA_PASO<>'' 
			BEGIN
				SET @hor_prog=@HORA_PASO
				SET @fec_prog = DATEADD(DAY, @DIA,@fec_prog) 
			END

			set @Detalle = '1|  |ZZ |1|POR EL SERVICIO DE TRANSPORTE DE LA RUTA ' + @origen  + '-' +  @destino + '/ SERVICIO: ' + @ser_prog  +'/ NRO ASIENTO:' + 
			RIGHT('00'+LTRIM(@asiento),2)
			+ '/ PASAJERO: ' + @nom_cli  + ' / DNI: ' + @doc_cli + ' / FECHA VIAJE: ' + CONVERT(varchar, @fec_prog,103) + ' / HORA VIAJE: ' + @hor_prog + '  /PRECIO: ' + LTRIM(@total) + '|'+
			CASE WHEN @igv <> 0	THEN ltrim(@total/(1.0+@igv)) ELSE LTRIM(@total) END + '|' + 
			LTRIM(@total)  + '|'+
			CASE WHEN @igv <> 0	THEN '10' ELSE '30' END+'|'+ 
			CASE WHEN @igv <> 0	THEN ltrim(@total-(@total/(1.0+@igv))) ELSE '0' END+
			'||0.00|0.00|' + 
			CASE WHEN @igv <> 0	THEN ltrim(@total/(1.0+@igv)) ELSE LTRIM(@total) END
			+ '|0.00|0.00' 

			--set @Detalle = '1|  |ZZ |1|POR EL SERVICIO DE TRANSPORTE DE LA RUTA ' + @origen  + '-' +  @destino + '/ SERVICIO: ' + @ser_prog  +'/ NRO ASIENTO: ' + 
			--RIGHT('00'+LTRIM(@asiento),2)
			--+ ' / PASAJERO: ' + @nom_cli  + ' / DNI: ' + @doc_cli + ' / FECHA VIAJE: ' + CONVERT(varchar, @fec_prog,103) + ' / HORA VIAJE: ' + @hor_prog + '  /1/' + LTRIM(@total) + '|' + LTRIM(@total) + '|' + LTRIM(@total) + '|30|' 
			--+ '0||0.00|0.00|' + LTRIM(@total) + '|0.00|0.00' ---ltrim(@total-cast(@total/1.18 as numeric(18,2))) +
		END
		if @CODI_DOCUMENTO='01' 
		BEGIN

			--******************** TODOS LOS CLIENTES  ******************************	
			select @raz =REPLACE(Razon_Social,'|','') ,@dir_ruc=REPLACE(Direccion ,'|','')from Tb_Ruc where Ruc_Cliente= @ruc
			set @Receptor='6|' +@ruc +'|'+ @raz +'||' + @dir_ruc +'||||||'
			--***********************************************************************
			
			--******************** FLORES  ******************************	
			--select @raz = REPLACE(Razon_Social,'|',''),@dir_ruc=REPLACE(Direccion ,'|','') from Tb_Ruc where Ruc_Cliente= @ruc
			--If @ruc = '20131257750'
			--BEGIN
			--	set @Receptor='6|' +@ruc +'|'+ @raz +'||' + 'AV. DOMINGO CUETO NRO 120 LIMA-LIMA-JESUS MARIA' +'||||||'
			--END
			--ELSE
			--BEGIN
			--	set @Receptor='6|' +@ruc +'|'+ @raz +'||' + @dir_ruc +'||||||'
			--END
			--***********************************************************************
		END
		ELSE
		BEGIN
			IF @TDOC_C='01' SET @TDOC_C_SUNAT='1' ELSE IF @TDOC_C='03' SET @TDOC_C_SUNAT='4' ELSE IF @TDOC_C='07' SET @TDOC_C_SUNAT='7' ELSE SET @TDOC_C_SUNAT='0' 
			IF @TDOC_C_SUNAT='0'
			BEGIN
				set @Receptor='0|0|-||||||||'
			END
			ELSE
			BEGIN
				set @Receptor=@TDOC_C_SUNAT+'|'+ @doc_cli +'|'+  REPLACE( @nom_cli,'|','')  +'||||||||'
			END
		END

		select '1' error,@Cabecera Cabecera,@datosAdicionales datosAdicionales,@Detalle Detalle,@Receptor as Receptor
	END TRY
	BEGIN CATCH  
		select '0' error,'' Cabecera,'' datosAdicionales,'' Detalle,'' as Receptor
	END CATCH
end