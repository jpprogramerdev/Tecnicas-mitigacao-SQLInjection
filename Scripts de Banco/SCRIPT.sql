CREATE DATABASE CRUD_TG;

USE CRUD_TG;

#Trigrama = TPU
CREATE TABLE TIPOS_USUARIO(
	TPU_ID INT AUTO_INCREMENT,
    TPU_TIPO VARCHAR(250) NOT NULL,
    CONSTRAINT PK_TIPOS_USUARIOS PRIMARY KEY(TPU_ID)
); 

#Trigrama = USU
CREATE TABLE USUARIOS(
	USU_ID INT AUTO_INCREMENT,
    USU_NOME VARCHAR(250) NOT NULL,
    USU_SENHA VARCHAR(100) NOT NULL,
    USU_CPF CHAR(11) NOT NULL,
    USU_TPU_ID INT NOT NULL,
	CONSTRAINT PK_USUARIOS PRIMARY KEY(USU_ID),
	CONSTRAINT FK_USUARIOS_TIPOS_USUARIOS FOREIGN KEY(USU_TPU_ID) REFERENCES TIPOS_USUARIO(TPU_ID)
);

INSERT INTO TIPOS_USUARIO(TPU_TIPO) VALUES('Administrador');
INSERT INTO TIPOS_USUARIO(TPU_TIPO) VALUES('Cliente'); 
INSERT INTO TIPOS_USUARIO(TPU_TIPO) VALUES('Fornecedor');