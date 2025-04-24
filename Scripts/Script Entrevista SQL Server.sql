CREATE DATABASE AgendaDB;
GO

USE AgendaDB;
GO

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Usuario NVARCHAR(50) NOT NULL UNIQUE,
    Senha NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Compromissos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    Titulo NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(500),
    DataCompromisso DATETIME NOT NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);
GO

SET DATEFORMAT YMD;

INSERT INTO Usuarios (Nome, Usuario, Senha)
VALUES ('Admin', 'admin', '1234');

INSERT INTO Compromissos (UsuarioId, Titulo, Descricao, DataCompromisso)
VALUES (1, 'Cabelo', 'Fazer barba e cabelo na rua de baixo', '2025-04-25 08:30:00');
