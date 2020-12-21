

Go
Alter Table WebOrders
Add Email_Cliente	Varchar(200)
Go

Go
Alter Table VentaWeb
Add Indi_Anulado Bit
Go

Go
Alter Table VentaWeb
Add FechaWeb_Anulado SmallDateTime
Go

Go
Alter Table VentaWeb
Add Voucher Varchar(50)
Go

Go
Alter Table WebTarjetaPagoTipo
Add Flag_Pasarela	Varchar(5)
Go

Go
Alter Table WebTarjetaPagoTipo
Add Titulo	Varchar(100)
Go

Go
Alter Table tb_cliente_pasajes
Add id_oficinaemision int
Go

Go
Alter Table tb_cliente_pasajes
Add id_puntoembarque int
GO

Go
ALTER TABLE WebOrders ALTER COLUMN Id_Users Int
Go

Go
ALTER TABLE Users ALTER COLUMN Id_Users Int
Go


sp_help Users
Go
Create Table Tb_Embarque(
Nro_Viaje			Int,
Codi_Sucursal		smallint,
Hora_Embarque		Varchar(10),
Tipo_Embarque		Char(1)
)
Go

Go
----Lima - Huaraz - 01:00PM - Embarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(16,230,'11:45AM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(16,70,'01:00PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(16,4,'01:30PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(16,265,'02:45PM',1)

----Lima - Huaraz - 01:00PM - Desmbarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(16,2,'',2)


----Lima - Huaraz - 10:30PM
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(56,15,'09:00PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(56,70,'10:30PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(56,4,'11:00PM',1)

----Lima - Huaraz - 10:30PM - Desmbarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(56,2,'',2)

----Lima - Sullana/Piura - 06:30PM - Embarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(98,15,'04:30PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(98,70,'06:30PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(98,4,'07:00PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(98,265,'08:30PM',1)

----Lima - Sullana/Piura - 06:30PM - Desmbarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(98,60,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(98,17,'',2)

----Lima - Chiclayo - 06:45PM - Embarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(112,230,'05:00PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(112,70,'06:45PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(112,265,'07:15PM',1)

----Lima - Chiclayo - 06:45PM - Desembarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(112,46,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(112,23,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(112,39,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(112,40,'',2)


----Lima - Chiclayo - 08:45PM - Embarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(113,230,'07:00PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(113,70,'08:45PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(113,265,'09:15PM',1)

----Lima - Chiclayo - 08:45PM - Desembarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(113,46,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(113,23,'',2)

----Lima - Chiclayo - 10:00PM - Embarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(115,15,'08:30PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(115,70,'10:00PM',1)

----Lima - Chiclayo - 10:00PM - Desembarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(115,46,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(115,23,'',2)

----Huaraz - Lima - 10:30AM - Embarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(42,2,'10:30AM',1)

----Huaraz - Lima - 10:30AM - Desembarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(42,0,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(42,70,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(42,15,'',2)


----Huaraz - Lima - 10:20PM - Embarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(18,2,'10:30AM',1)

----Huaraz - Lima - 10:20PM - Desembarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(18,0,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(18,70,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(18,15,'',2)

----Chiclayo - Lima - 06:00PM - Embarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(95,46,'06:00PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(95,23,'06:20PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(95,39,'07:30PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(95,40,'08:00PM',1)

----Chiclayo - Lima - 06:00PM - Desembarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(95,0,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(95,70,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(95,230,'',2)

----Chiclayo - Lima - 08:00PM - Embarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(96,46,'08:00PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(96,23,'08:20PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(96,39,'09:30PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(96,40,'10:00PM',1)

----Chiclayo - Lima - 08:00PM - Desembarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(96,0,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(96,70,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(96,230,'',2)

----Chiclayo - Lima - 10:00PM - Embarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(23,46,'10:00PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(23,23,'10:20PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(23,39,'11:30PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(23,40,'12:00AM',1)

----Chiclayo - Lima - 10:00PM - Desembarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(23,0,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(23,70,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(23,15,'',2)

----Sullana/Piura - Lima - 05:30PM - Embarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(32,59,'05:30PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(32,60,'06:00PM',1)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(32,17,'07:00PM',1)

----Sullana/Piura - Lima - 05:30PM - Desembarque
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(32,0,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(32,4,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(32,70,'',2)
Insert Into Tb_Embarque(Nro_Viaje		, Codi_Sucursal		, Hora_Embarque		, Tipo_Embarque		)
Values(32,15,'',2)
Go


