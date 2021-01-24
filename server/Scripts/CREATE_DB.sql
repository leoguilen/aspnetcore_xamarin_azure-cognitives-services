CREATE DATABASE [SmartAuth];
USE [SmartAuth];

CREATE TABLE [dbo].[Usuario](
	[Id] [uniqueidentifier] NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Sobrenome] [varchar](50) NOT NULL,
	[DataNascimento] [datetime2](7) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Rg] [varchar](10) NOT NULL,
	[Cpf] [varchar](15) NOT NULL,
	[DataCadastro] [datetime2](7) NOT NULL
)
