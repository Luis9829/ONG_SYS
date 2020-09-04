--USE master
--DROP DATABASE dbONG_VCompleta2


create database dbONG_VCompleta2
go
use  dbONG_VCompleta2
go


create table tblTipoProducto(
idTipoProducto int identity(1,1) Primary Key Not null  ,
NombreTipoP nvarchar(100) Not null,
)

create table tblTipoServicio (
idTiposervicio  int identity(1,1) primary key not null,
nombreTiposervicio varchar(50)

)
create table tblServicio(
idServicio int identity(1,1) primary key not null,
nombreServicio varchar(50),
valorServicio float,
idTiposervicio int,
constraint FK_Servicio foreign key (idTiposervicio) references tblTipoServicio(idTiposervicio),

)

create table tblProvincia(
idProvincia int identity(1,1) primary key not null,
NombreProvincia nvarchar(100) Not null,

)
create table tblProveedores(
idProveedor int identity(1,1) not null primary key,
NombreProveedor nvarchar(100) Not null,
rucProveedor nvarchar(13) not null,
idProvincia int not null,
ciudad nvarchar(100)not null,
direccionProveedor nvarchar(100) not null,
telefono nvarchar(100) not null,
constraint FK_Provincia foreign key (idProvincia) references tblProvincia(idProvincia)
on delete cascade
on update cascade,
)



create table tblProductos(
idProducto int identity(1,1) Primary Key NOT NULL ,
NombreProducto nvarchar (100) Not null,
idTipoProducto int,
idProveedor int,
Marca nvarchar (100) not null,
Precio float not null,
Stock int not null,
constraint FK_TipoProducto foreign key (idTipoProducto) references tblTipoProducto(idTipoProducto)
ON DELETE CASCADE
ON UPDATE CASCADE,
constraint FK_idProveedor foreign key (idProveedor) references tblProveedores(idProveedor)
ON DELETE CASCADE
ON UPDATE CASCADE
)


create table tblTipoCliente (
 intTipocliente int identity(1,1) primary key not null,
 nombreTipocliente varchar(50),


)
create table tblCliente(
idCliente int identity(1,1) primary key not null,
intTipocliente int,
nombreCliente varchar(50),
apellidoCliente varchar(50),
cedulaCliente varchar(13),
telefonoCliente varchar(10),
direcionCliente varchar(50),
correoCliente varchar(50),

constraint FK_tipoCliente foreign key (intTipocliente) references tblTipoCliente(intTipocliente),

)
create table tblUsuario(

idUsuario int identity(1,1) primary key not null,
nombreUsuario varchar(50),
contraseniaUsuario varchar(50)

)
create table tblFactura(
idFactura int identity(1,1) primary key not null,
fechaFactura date default getdate(),
idCliente int ,
idUsuario int,
subtotal money,
iva money,
total money,

constraint FK_Usuario foreign key (idUsuario) references tblUsuario(idUsuario),
constraint FK_Cliente foreign key (idCliente) references tblCliente(idCliente)
)

create table tblDetalle(
idDetalle int identity(1,1) primary key not null,
idProducto int,
idServicio int,
idFactura int,
cantidadD int,
importe money,


 constraint FK_Dproducto foreign key (idProducto) references tblProductos(idProducto),
  constraint FK_Dservicio foreign key (idServicio) references tblServicio(idServicio),
    constraint FK_DFactura foreign key (idFactura) references tblFactura(idFactura)
)

create table tblEmpleado(
idEmpleado int identity(1,1) primary key not null,
nombreEmpleado varchar(20),
)


insert into tblTipoCliente values ('Estándar'),('Por contrato')
insert into tblTipoProducto values ('Limpieza'),('Mantenimiento')
insert into tblTipoServicio values ('Alineación'),('Balanceo'),('Cambio de Aceite'),('Enllantaje'),('Lavado'),('N/A')

insert into tblProvincia
values
('Azuay'), 
('Bolívar'),
('Cañar'), 
('Carchi'), 
('Chimborazo'), 
('Cotopaxi'), 
('El Oro'), 
('Esmeraldas'), 
('Galápagos'),
('Guayas'), 
('Imbabura'),
('Loja'), 
('Los Ríos'), 
('Manabí'), 
('Morona Santiago'), 
('Napo'), 
('Orellana'), 
('Pastaza'), 
('Pichincha'), 
('Santa Elena'), 
('Santo Domingo de los Tsáchilas'),
('Sucumbíos'), 
('Tungurahua'), 
('Zamora Chinchipe')


insert into tblTipoProducto
values
('Limpieza'),
('Mantenimiento'),
('Repuestos')

insert into tblProveedores
values
('Lubricantes Ecuador S.A','1756234578903',4,'Quito','Av.Ecuatoriana y calle Alonso','2955156'),
('Distribuidora Alonso S.A','1756321278903',6,'Ambato','Av.Simon Bolivar y calle Nayon','0978456732'),
('Compañia de repuestos La Ecuatoriana S.A','1756321272907',8,'Cuenca','Autopista General Rumiñahui y Puente 8','78291371')


insert into tblProductos 
values
('Lubricante',2,1,'Valvoline',50.99,7),
('Jabon de autos',1,2,'Soap Cars',30.50,9),
('Bujias',3,3,'Fujikawa cars maintenance',25.00,10)


insert into tblServicio
values
('Enllantaje',50.89,2),
('Filtrado',50.89,1),
('Lavado',50.89,5),
('N/A',null,6)

insert into tblCliente
values
(1,'Juan','Acurio','1726543478','29551345','La Bota y calle A','juanito@gmail.com'),
(2,'Alvaro','Estrada','1873664376','2846742','La Floresta','alv_69@hotmail.com'),
(1,'Pablo','Jimenez','0753675275','2834562','Santa Clara','pablojime@live.com'),
(1,'Hernan','Narvaez','1800328532','24174983','El Tejar','hernanarvaez@hotmail.com')

insert into tblUsuario values
('Carlos Torres','ongsystem')


GO
--Trigger Importe
CREATE TRIGGER tr_importe ON tblDetalle AFTER INSERT
AS
BEGIN
DECLARE @cantidad INT SELECT @cantidad = cantidadD FROM INSERTED
DECLARE @productoID INT SELECT @productoID = idProducto FROM inserted
DECLARE @servicioID INT SELECT @servicioID = idServicio FROM inserted
DECLARE @facturaID INT SELECT @facturaID = idFactura FROM inserted
IF(@servicioID IS NULL)
BEGIN
UPDATE tblDetalle SET importe = @cantidad*(SELECT Precio FROM tblProductos WHERE tblProductos.idProducto=@productoID) WHERE idProducto=@productoID
END
ELSE
BEGIN
UPDATE tblDetalle SET importe = @cantidad*(SELECT valorServicio FROM tblServicio WHERE tblServicio.idServicio=@servicioID) WHERE idServicio=@servicioID 
END
UPDATE tblFactura SET subtotal = (SELECT SUM(importe) FROM tblDetalle WHERE tblDetalle.idFactura=@facturaID) WHERE idFactura = @facturaID
UPDATE tblFactura SET iva = subtotal*0.12, total= subtotal*1.12 WHERE idFactura = @facturaID
END
GO

--SP Ingreso Nueva Factura
CREATE PROCEDURE sp_NuevaFactura
@clienteID int,
@usuarioID int,
@subtotal money = 0,
@iva money = 0,
@total money = 0
AS
insert into tblFactura (idCliente,idUsuario) values (@clienteID,@usuarioID)
GO

--SP Ingreso Detalle
CREATE PROCEDURE sp_IngresoProducto
@productoID int,
@facturaID int,
@cantidad int
AS
insert into tblDetalle (idProducto, idFactura, cantidadD) values (@productoID,@facturaID,@cantidad)
GO

CREATE PROCEDURE sp_IngresoServicio
@servicioID int,
@facturaID int,
@cantidad int
AS
insert into tblDetalle (idServicio, idFactura, cantidadD) values (@servicioID,@facturaID,@cantidad)
GO


--SP Ingreso Cliente
CREATE PROCEDURE sp_IngresoCliente
@TipoClienteID int,
@nombre varchar(50),
@apellido varchar(50),
@cedula varchar(50),
@telefono varchar(10),
@direccion varchar(50),
@correo varchar(50)
AS
insert into tblCliente (intTipocliente,nombreCliente,apellidoCliente,cedulaCliente,telefonoCliente,direcionCliente,correoCliente) values (@TipoClienteID,@nombre,@apellido,@cedula,@telefono,@direccion,@correo)
GO

--SP ACTUALIZAR CLIENTE
CREATE PROCEDURE sp_ActualizarCliente
@clienteID int,
@TipoClienteID int,
@nombre varchar(50),
@apellido varchar(50),
@cedula varchar(50),
@telefono varchar(10),
@direccion varchar(50),
@correo varchar(50)
AS
UPDATE tblCliente SET intTipocliente = @TipoClienteID, nombreCliente=@nombre, 
apellidoCliente=@apellido,cedulaCliente=@cedula, telefonoCliente=@telefono,
direcionCliente=@direccion, correoCliente=@correo WHERE idCliente=@clienteID

--SP ELIMINAR CLIENTE
GO
CREATE PROCEDURE sp_EliminarCliente
@clienteID int
AS
DELETE FROM tblCliente WHERE idCliente=@clienteID



go

----------TABLAS--------------
--SELECT * FROM tblTipoCliente
--SELECT * FROM tblTipoProducto
--SELECT * FROM tblTipoServicio
--SELECT * FROM tblServicio
--SELECT * FROM tblProductos
--SELECT * FROM tblCliente
--SELECT * FROM tblDetalle
--SELECT * FROM tblFactura
--GO

--Vistas
go
CREATE VIEW vClientes AS
SELECT idCliente,nombreCliente +' '+ apellidoCliente AS 'Cliente' ,cedulaCliente AS 'Cedula',telefonoCliente AS 'Telefono',direcionCliente AS 'Direccion',correoCliente AS 'Correo',nombreTipocliente AS 'Tipo' 
FROM tblCliente JOIN tblTipoCliente ON (tblCliente.intTipocliente=tblTipoCliente.intTipocliente)
GO

CREATE VIEW vProductos AS
SELECT tblProductos.NombreProducto,tblProductos.Marca,tblProductos.Precio,tblProductos.Stock,tblTipoProducto.NombreTipoP,tblProveedores.NombreProveedor 
FROM tblProductos JOIN tblProveedores ON (tblProductos.idProveedor=tblProveedores.idProveedor) JOIN tblTipoProducto ON (tblTipoProducto.idTipoProducto = tblProductos.idTipoProducto)
GO

CREATE VIEW vServicios AS
SELECT idServicio,nombreServicio,valorServicio,nombreTiposervicio FROM tblServicio JOIN tblTipoServicio ON (tblServicio.idServicio=tblTipoServicio.idTiposervicio) 
GO



CREATE VIEW vEncabezado AS
SELECT tblFactura.idFactura AS 'ID', fechaFactura AS 'Fecha', nombreCliente +' '+ apellidoCliente AS 'Nombre Cliente', cedulaCliente AS 'Cedula',
direcionCliente AS 'Direccion', telefonoCliente AS 'Telefono', nombreUsuario AS 'Vendedor',
subtotal AS 'Subtotal', iva AS 'IVA (12%)', total AS 'Total' 
FROM tblFactura JOIN tblCliente ON (tblFactura.idCliente=tblCliente.idCliente) JOIN tblUsuario ON (tblFactura.idUsuario=tblUsuario.idUsuario) JOIN tblDetalle ON (tblDetalle.idFactura=tblFactura.idFactura) JOIN tblProductos ON(tblDetalle.idProducto=tblProductos.idProducto) JOIN tblServicio ON (tblServicio.idServicio=tblDetalle.idProducto) JOIN tblTipoServicio ON (tblServicio.idServicio=tblTipoServicio.idTiposervicio)
GO

CREATE VIEW vDetalle AS
SELECT 
    tblDetalle.idFactura AS 'iD', 
    tblDetalle.idDetalle '#Detalle',
    'Detalle' = CASE 
        WHEN tblDetalle.idServicio IS NULL THEN tblProductos.NombreProducto 
        WHEN tblDetalle.idProducto IS NULL THEN tblServicio.nombreServicio 
    END, 
	'Valor Unitario' = CASE 
        WHEN tblDetalle.idServicio IS NULL THEN tblProductos.Precio 
        WHEN tblDetalle.idProducto IS NULL THEN tblServicio.valorServicio 
    END,
	tblDetalle.cantidadD AS 'Cantidad', tblDetalle.importe AS 'Importe' 
	FROM tblDetalle LEFT JOIN tblProductos ON tblDetalle.idProducto=tblProductos.idProducto 
	LEFT JOIN tblServicio ON tblDetalle.idServicio=tblServicio.idServicio
GO
---servicios

create view VMostrar
as
select idServicio, nombreTiposervicio, nombreServicio,valorServicio from tblServicio join tblTipoServicio
on tblServicio.idTiposervicio=tblTipoServicio.idTiposervicio;


go

----Ver Cliente
--SELECT * FROM vClientes
----Ver Productos
--SELECT * FROM vProductos
----Ver Servicios
--SELECT * FROM vServicios
----Diseño de Factura
--SELECT * FROM vEncabezado
--SELECT * FROM vDetalle

----SP Ingreso Factura (idCliente,idUsuario)
--EXECUTE sp_NuevaFactura 2,1 
----SP Ingreso Producto (idProducto, idFactura, cantidad)
--EXECUTE sp_IngresoProducto 1,1,3
----SP Ingreso Servicio (idServicio, idFactura, cantidad)
--EXECUTE sp_IngresoServicio 1,1,1
--GO

--SP Ver Clientes
CREATE PROCEDURE sp_VerClientes
AS
SELECT * FROM vClientes
GO
----- Proveedores Productos------

create proc MostrarProductos
as
select *from tblProductos
go
exec MostrarProductos
go
create proc MostrarTipodeProductos
as
select *from tblTipoProducto
go

create proc MostrarProveedores
as
select *from tblProveedores
go

create proc MostrarProvincias
as
select* from tblProvincia
go
 exec MostrarProvincias

 --- Procedimientos de ingreso de proveedores------
go
create proc AgregarProveedor
@NombreProveedor nvarchar(100) ,
@rucProveedor nvarchar(100) ,
@idProvincia int ,
@ciudad nvarchar(100),
@direccionProveedor nvarchar(100),
@telefono nvarchar(100) 
as
insert into tblProveedores values (@NombreProveedor,@rucProveedor,@idProvincia,@ciudad,@direccionProveedor,@telefono)
go

create proc EliminarProveedor
@idProveedor int
as
delete from  tblProveedores where idProveedor=@idProveedor
go

create proc ActualizarProveedor
@NombreProveedor nvarchar(100) ,
@rucProveedor nvarchar(13) ,
@idProvincia int ,
@ciudad nvarchar(100),
@direccionProveedor nvarchar(100),
@telefono nvarchar(100) ,
 @idProveedorActualizar int
as
update tblProveedores set NombreProveedor= @NombreProveedor, rucProveedor=  @rucProveedor,idProvincia=@idProvincia,ciudad=@ciudad,direccionProveedor= @direccionProveedor,telefono=@telefono
where idProveedor=@idProveedorActualizar
go

create Procedure Mostrar_Proveedores3
as
SELECT tblProveedores.idProveedor, tblProveedores.NombreProveedor,tblProveedores.rucProveedor,tblProvincia.NombreProvincia,tblProveedores.ciudad,tblProveedores.direccionProveedor,tblProveedores.telefono
FROM tblProveedores
INNER JOIN tblProvincia
ON tblProvincia.idProvincia=tblProveedores.idProvincia
go
--exec Mostrar_Proveedores3
-------------------------------SERVICIOS-----------------------------------------------------

go
create proc Mostrarservicios
as
select *from VMostrar;
go



create proc InsertarServicios
@nombreServicio varchar(100),
@valorServicio float,
@tipoServicio int 
as
insert into tblServicio values(@nombreServicio ,@valorServicio,@tipoServicio)
go


 
create proc EliminarServicio 
@idServicio int
as
delete from tblServicio where idServicio=@idServicio
go

create proc EditarServicio
@nombreServicio varchar(100),
@valorServicio float,
@tipoServicio int,
@idServicio int
as
update tblServicio set nombreServicio=@nombreServicio,valorServicio=@valorServicio, idTiposervicio=@tipoServicio
where idServicio=@idServicio;
go
-- ----------------------------------Verificacion de usuarios-------------------------------------------------

create proc verificarUsuario
@nombreUsuario varchar(50),
@contraseniaUsuario varchar(50)

as
--declare @VU smallint 
select*from tblUsuario where nombreUsuario=@nombreUsuario and contraseniaUsuario =@contraseniaUsuario

go 
--select*from tblUsuario;
--exec verificarUsuario 'Carlos Torres', 'ongsystem' 
--SP Ingreso Nueva Factura
-------------------------------------------Facturacion ---------------------------------
create proc Mostrardetalle
as
select *from vDetalle;
go
 create proc MostrarSIT
 @id int
 as
 select Subtotal ,[IVA (12%)], Total from vEncabezado  where @id= iD;
 go 
 create proc MostrarCliente
 as
 select*from vClientes;
 go

 create proc IngresarProductos
@NombreProducto nvarchar (100)  ,
@idTipoProducto int ,
@idProveedor int,
@Marca nvarchar (100) ,
@Precio float,
@Stock int 
as
insert into tblProductos values (@NombreProducto,@idTipoProducto,@idProveedor,@Marca,@Precio,@Stock)
go
exec IngresarProductos XC,2,3,"Marco Polo",50.99,34
select* from tblProductos
create proc EliminarProducto
@idProducto int
as
delete from  tblProductos where idProducto=@idProducto
go
--- Productos
create proc EditarProductos
@NombreProducto nvarchar (100)  ,
@Marca nvarchar (100) ,
@idTipoProducto int,
@idProveedor int,
@Precio float,
@Stock int, 
@idProductoaActualizar int
as
update tblProductos set NombreProducto=@NombreProducto,idTipoProducto=@idTipoProducto,idProveedor=@idProveedor,Marca=@Marca,Precio=@Precio,Stock=@Stock
where idProducto=@idProductoaActualizar
go

create procedure Mostrar_Productos
as
SELECT tblProductos.idProducto, tblProductos.NombreProducto,tblTipoProducto.NombreTipoP,tblProveedores.NombreProveedor,tblProductos.Marca,tblProductos.Precio,tblProductos.Stock
FROM tblProductos
INNER JOIN tblTipoProducto ON tblProductos.idTipoProducto = tblTipoProducto.idTipoProducto
INNER JOIN tblProveedores on tblProductos.idProveedor =tblProveedores.idProveedor 
go

create proc MostrarTipoProductos
as
select* from tblTipoProducto
go

