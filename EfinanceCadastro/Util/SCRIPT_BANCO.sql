CREATE DATABASE EFINANCE;
GO

USE EFINANCE;
GO

CREATE TABLE estado(
idestado INT PRIMARY KEY IDENTITY,
codufestado INT,
nomeestado VARCHAR(60),
ufestado char(2),
statusestado BIT DEFAULT 'TRUE');

CREATE TABLE cidade(
idcidade INT PRIMARY KEY IDENTITY,
idestado INT NOT NULL,
nomecidade VARCHAR(60) NOT NULL,
codigoibgecidade INTEGER,
statuscidade BIT DEFAULT 'TRUE',
CONSTRAINT fk_cidadeestado FOREIGN KEY (idestado) REFERENCES estado(idestado));

CREATE TABLE cliente(
idcliente INT PRIMARY KEY IDENTITY,
idcidade INT NOT NULL,
nomecliente VARCHAR(60) not null,
cpfcnpjcliente VARCHAR(15),
telefonecliente VARCHAR(13),
statuscliente BIT DEFAULT 'TRUE',
CONSTRAINT fk_clientecidade FOREIGN KEY(idcidade) REFERENCES cidade(idcidade));




