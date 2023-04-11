CREATE DATABASE BookShop;
GO
USE BookShop;
GO
----CREATING OF THE TABLES----------------------
--TABLES WHIT PK
CREATE TABLE Clientes(
  Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  NombreCompleto VARCHAR(30) NOT NULL,
  DUI CHAR(10) NULL,
  Telefono CHAR(10) NULL,
  Direccion VARCHAR(100) NULL,
  Estado TINYINT NOT NULL
);
GO
CREATE TABLE Roles(
  Id TINYINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  Nombre VARCHAR(30) NOT NULL,
  Estado TINYINT NOT NULL
);
GO
CREATE TABLE Marcas(
  Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, 
  Nombre VARCHAR(30) NOT NULL,
  Estado TINYINT NOT NULL
);
GO
CREATE TABLE Categorias(
  Id TINYINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  Nombre VARCHAR(30) UNIQUE NOT NULL, 
  Estado TINYINT NOT NULL
);

GO
CREATE TABLE Proveedores(
  Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  Nombre VARCHAR(30) NOT NULL,
  Direccion VARCHAR(100) NOT NULL,
  Telefono CHAR(10) NOT NULL,
  Estado TINYINT NOT NULL
);
GO
--TABLES WHIT FK(RELATIONS)
CREATE TABLE Usuarios(
  Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  IdRol TINYINT NOT NULL REFERENCES Roles,
  Nombre VARCHAR(30) NOT NULL,
  Apellido VARCHAR(30) NOT NULL,
  NombreUsuario VARCHAR(10) UNIQUE NOT NULL,
  ClaveUsuario VARCHAR(250) NOT NULL,
  Estado TINYINT NOT NULL
);
GO

CREATE TABLE Productos(
  Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  IdCategoria TINYINT NOT NULL REFERENCES Categorias,
  IdMarca INT NOT NULL REFERENCES Marcas,
  Nombre VARCHAR(30) NOT NULL,
  Descripcion VARCHAR(250) NOT NULL,
  Img IMAGE NOT NULL,
  Estado TINYINT NOT NULL
);
GO
CREATE TABLE Compras(
  Id BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  IdUsuario INT NOT NULL REFERENCES Usuarios,/*foranea*/
  IdProveedor INT NOT NULL REFERENCES Proveedores,/*foranea*/
  FechaRegistro DATETIME NOT NULL,
  Estatus TINYINT NOT NULL,
  Estado TINYINT NOT NULL
);

GO
CREATE TABLE DetallesCompra(
  Id BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  IdProducto INT NOT NULL REFERENCES Productos,
  IdCompra BIGINT NOT NULL REFERENCES Compras,
  Precio DECIMAL(7,2) NOT NULL,
  Cantidad INT NOT NULL,
  SubTotal DECIMAL(9,2) NOT NULL,
  Estado TINYINT NOT NULL
);
GO

CREATE TABLE Ventas(
	Id BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	IdUsuario INT NOT NULL,
	IdCliente INT NOT NULL,
	FechaRegistro DATETIME NOT NULL,
	Estatus TINYINT NOT NULL, --Estatus == CREADO =1, CERRADO = 2, ANULADO = 3
	Pago VARCHAR(50)  NOT NULL,
	Estado TINYINT NOT NULL,
	--definicion de llave foranea
	FOREIGN KEY(IdUsuario) REFERENCES Usuarios(Id),
    FOREIGN KEY(IdCliente) REFERENCES Clientes(Id),
);
GO
--Tablas con llaves foreneas
CREATE TABLE DetallesVenta(
	Id BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	IdVenta BIGINT NOT NULL,
	IdProducto INT NOT NULL,
	Precio DECIMAL(7,2) NOT NULL,
	Cantidad INT NOT NULL,
	SubTotal DECIMAL(9,2) NOT NULL,
	Estado TINYINT NOT NULL
	--definicion de llave foranea
	FOREIGN KEY(IdVenta) REFERENCES Ventas(Id),
	FOREIGN KEY(IdProducto) REFERENCES Productos(Id),
);
go
CREATE TABLE Inventario( 
  Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  IdProducto INT NOT NULL REFERENCES Productos,
  PrecioCompra DECIMAL(6,2) NOT NULL,
  PrecioVenta DECIMAL(6,2) NOT NULL,
  FechaHoraIngreso DATETIME NOT NULL,
  Cantidad INT NOT NULL,
  Estado TINYINT NOT NULL
);
GO
---PRACTICE WITH DML COMMANDS
--INSERT RECORDS
INSERT INTO Roles(Nombre,Estado)
VALUES('Administrador', 1),
      ('Auditor', 1),
	  ('Vendedor', 1),
      ('Jefe', 1);
SELECT*FROM Roles;
GO
INSERT INTO Usuarios(IdRol, Nombre ,Apellido,NombreUsuario, ClaveUsuario ,Estado)
VALUES(1,'Alexander','Rivera','ARivera','1111', 1),
	  (1,'Ronal','Alas','RAlas','2222', 1),
	  (1,'Jaime','Perez','JPerez','3333', 1),
	  (1,'Alexis','Flores','AFlores','4444', 1),
      (2,'David','Cortez','DCortez','2222', 1),
	  (3,'Monica','Flores','MFlores','3333', 1);
SELECT*FROM Usuarios;
GO
INSERT INTO Marcas(Nombre, Estado)
VALUES('Norma',1),
	  ('Facela',1),
	  ('Big',1);
GO
SELECT * FROM Marcas;
GO
INSERT INTO Categorias(Nombre, Estado)
VALUES('Libro', 1),
	  ('Cuaderno',1),
	  ('Lapiz',1);
GO
SELECT * FROM Categorias;
GO
INSERT INTO Proveedores(Nombre, Direccion, Telefono, Estado)
VALUES('Norma Company','San Salvador', 25241536, 1),
		('Facela Company','San Salvador', 25241536, 1),
		('Big Company','San Salvador', 25241536, 1);
GO
SELECT * FROM Proveedores;
GO
INSERT INTO Compras(IdUsuario, IdProveedor, FechaRegistro, Estatus, Estado)
VALUES(3,1,'07/02/2023 14:37:00',1,1),
      (3,1,'07/02/2023 14:37:00',1,1);
GO
SELECT * FROM Compras;
GO
INSERT INTO Clientes(NombreCompleto, DUI, Telefono,Direccion, Estado)
VALUES('VARIOS','000000-0','0000-0000','San Salvador, ES',1);
GO
SELECT * FROM Clientes;


-- Actualizar un registro de Usuarios
UPDATE Usuarios
SET Nombre = 'Pablo', NombreUsuario = 'PCortez'
WHERE Id = 2;
SELECT * FROM Usuarios;
GO
-- Eliminar un registro 
DELETE FROM Roles
WHERE Id = 4
SELECT*FROM Roles;
GO
--Consulta multitablas
SELECT u.Nombre, u.Apellido, r.Nombre
FROM Usuarios u, Roles r
WHERE u.IdRol = r.Id
