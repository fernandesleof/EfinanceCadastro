CREATE DATABASE EFINANCE;
GO

USE EFINANCE;
GO
--create table
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

--procedures
CREATE or ALTER PROCEDURE crudEstado(
@pidestado integer,
@pnome varchar(60),
@pcoduf int,
@puf char(2),
@ptipo char(1))
as
begin
	IF(@ptipo = 'I')BEGIN
	INSERT INTO estado(codufestado,nomeestado,ufestado)
	VALUES (@pcoduf,@pnome,@puf) 
	END
	IF(@ptipo = 'U') BEGIN
	UPDATE estado SET codufestado = @pcoduf , nomeestado = @pnome , 
		ufestado = @puf WHERE idestado = @pidestado 
	END
	IF(@ptipo = 'D') BEGIN
	DELETE FROM estado WHERE idestado = @pidestado
	END
end;


CREATE or ALTER PROCEDURE crudCidade(
@pidcidade integer,
@pidestado int,
@pnome varchar(60),
@pcodigoibge int,
@ptipo char(1))
as
begin
	IF(@ptipo = 'I')BEGIN
	INSERT INTO cidade( idestado , nomecidade , codigoibgecidade) 
	VALUES ( @pidestado , @pnome , @pcodigoibge) 
	END
	IF(@ptipo = 'U') BEGIN
	UPDATE cidade set idestado = @pidestado , nomecidade = @pnome ,
	codigoibgecidade = @pcodigoibge WHERE idcidade = @pidcidade 
	END
	IF(@ptipo = 'D') BEGIN
	DELETE FROM cidade WHERE idcidade = @pidcidade
	END
end;

CREATE or ALTER PROCEDURE crudCliente(
@pidcliente integer,
@pidcidade integer,
@pnome varchar(60),
@ptelefone varchar(15),
@pcpfcnpj varchar(15),
@ptipo char(1))
as
begin  
	IF(@ptipo = 'I')BEGIN
	INSERT INTO cliente(nomecliente,telefonecliente,cpfcnpjcliente,idcidade) 
	VALUES (@pnome,@ptelefone,@pcpfcnpj,@pidcidade)
	END
	IF(@ptipo = 'U') BEGIN
	UPDATE cliente SET  nomecliente = @pnome , telefonecliente = @ptelefone,            
	cpfcnpjcliente = @pcpfcnpj , idcidade = @pidcidade WHERE idcliente = @pidcliente  
	END
	IF(@ptipo = 'D') BEGIN
	UPDATE cliente SET statuscliente = 'FALSE' WHERE idcliente = @pidcliente
	END
end;

--view
create or alter view listarestado as 
select * from estado 
where statusestado = 'true'
go

create or alter view listarcidade as
select c.*, e.nomeestado as estado
from cidade c
inner join estado e
on e.idestado=c.idestado
where c.statuscidade = 'true'
go

create or alter view listarcliente as
select cl.*,ci.nomecidade as cidade ,e.nomeestado as estado ,e.ufestado as uf
from cliente cl 
inner join cidade ci
on cl.idcidade = ci.idcidade 
inner join estado e 
on e.idestado=ci.idestado
where cl.statuscliente='true'
go
 



