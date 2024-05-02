# API_CadastroDeEscolas
Aplicação de registo de escola - Desafio back-end
Esta é uma aplicação back-end, (API), feita na versão do .NET CORE 7.0
Funcionalidades:
1- Criar novas escola
2- Editar escola
3 - Eliminar escola
4 - Listar todas escolas cadastradas
5- Filtrar uma escola especifica
6 - Importar dados do excel para base de dados

Tecnologias Utilizadas:
Linguagem de Programação C#
Dapper
CQRS como Padrão Arquitectural
SGB SQL Server 2019

Para Execução do projecto:

Modelo planilha excel a ser importado:
A imagem abaixo ilustra a esttrutura do campo em excel na qual devem estar estrutura para que a importação funciona

![image](https://github.com/JoaoEduardoPequena/API_CadastroDeEscolas/assets/62374762/01c21bb0-ddc9-4b01-9d91-a3e3007f7d97)

Criar a base de dados:

CREATE DATABASE BD_Escolas

Criação da tabela:

	CREATE TABLE TbEscolas(
	  id int IDENTITY(1,1) NOT NULL primary key,
	  nome varchar(200) NOT NULL,
	  email [varchar](200) NOT NULL,
	  numerosalas [int] NOT NULL,
	  provincia varchar(100) NOT NULL
	)





