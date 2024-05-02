# API_CadastroDeEscolas
<p>Aplicação de registo de escola - Desafio back-end</p>
Esta é uma aplicação back-end, (API), feita na versão do .NET CORE 7.0

<ul> Funcionalidades
   <li>Criar novas escola</li>
   <li>Editar escola</li>
   <li>Eliminar escola</li>
   <li>Listar todas escolas cadastradas</li>
   <li>Filtrar uma escola especifica</li>
   <li>Importar dados do excel para base de dados</li>	
</ul>

<h6>Tecnologias Utilizadas:</h6>
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





