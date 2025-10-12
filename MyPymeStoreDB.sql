CREATE DATABASE MyPymeStore;
GO

USE MyPymeStore;
GO

-- Tabla: Usuario
CREATE TABLE Usuario (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre_usuario VARCHAR(100) NOT NULL,
    correo VARCHAR(150) UNIQUE NOT NULL,
    contrasena VARCHAR(255) NOT NULL,
    rol VARCHAR(20) CHECK (rol IN ('admin','cliente','emprendedor')) DEFAULT 'cliente',
    fecha_registro DATETIME DEFAULT GETDATE()
);
GO

-- Tabla: Cliente
CREATE TABLE Cliente (
    id INT IDENTITY(1,1) PRIMARY KEY,
    usuarioId INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    direccion NVARCHAR(255),
    FOREIGN KEY (usuarioId) REFERENCES Usuario(id)
);
GO

-- Tabla: Categoria
CREATE TABLE Categoria (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion NVARCHAR(255)
);
GO

-- Tabla: Producto
CREATE TABLE Producto (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    categoriaId INT NOT NULL,
    precio DECIMAL(10,2) NOT NULL,
    impuestosPorCompra DECIMAL(10,2) NOT NULL,
    stock INT DEFAULT 0,
    imagenUrl NVARCHAR(255),
    activo BIT DEFAULT 1,
    FOREIGN KEY (categoriaId) REFERENCES Categoria(id)
);
GO

-- Tabla: Pedido
CREATE TABLE Pedido (
    id INT IDENTITY(1,1) PRIMARY KEY,
    clienteId INT NOT NULL,
    fecha_pedido DATETIME DEFAULT GETDATE(),
    total DECIMAL(10,2) NOT NULL,
    estado VARCHAR(20) CHECK (estado IN ('pendiente','pagado','enviado','entregado','cancelado')) DEFAULT 'pendiente',
    FOREIGN KEY (clienteId) REFERENCES Cliente(id)
);
GO

-- Tabla: DetallePedido
CREATE TABLE DetallePedido (
    id INT IDENTITY(1,1) PRIMARY KEY,
    pedidoId INT NOT NULL,
    productoId INT NOT NULL,
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (pedidoId) REFERENCES Pedido(id),
    FOREIGN KEY (productoId) REFERENCES Producto(id)
);
GO


-- Usuarios
INSERT INTO Usuario (nombre_usuario, correo, contrasena, rol)
VALUES 
('admin', 'admin@mypymestore.com', 'admin123', 'admin'),
('juan123', 'juan@correo.com', '12345', 'cliente'),
('mariaemprende', 'maria@correo.com', '12345', 'emprendedor');
GO

-- Clientes
INSERT INTO Cliente (usuarioId, nombre, apellido, telefono, direccion)
VALUES
(2, 'Juan', 'Pérez', '8888-1234', 'San José, Costa Rica');
GO

-- Categorías
INSERT INTO Categoria (nombre, descripcion)
VALUES
('Tecnología', 'Artículos electrónicos y gadgets'),
('Ropa', 'Prendas de vestir y accesorios'),
('Artesanías', 'Productos hechos a mano');
GO

-- Productos
INSERT INTO Producto (nombre, categoriaId, precio, impuestosPorCompra, stock, imagenUrl, activo)
VALUES
('Audífonos Bluetooth', 1, 25000.00, 3250.00, 10, 'audifonos.jpg', 1),
('Camiseta universitaria', 2, 8000.00, 1040.00, 15, 'camiseta.jpg', 1),
('Pulsera artesanal', 3, 3500.00, 455.00, 20, 'pulsera.jpg', 1);
GO

-- Pedido
INSERT INTO Pedido (clienteId, total, estado)
VALUES
(1, 28500.00, 'pagado');
GO

-- Detalle de pedido
INSERT INTO DetallePedido (pedidoId, productoId, cantidad, precioUnitario, subtotal)
VALUES
(1, 1, 1, 25000.00, 25000.00),
(1, 3, 1, 3500.00, 3500.00);
GO
