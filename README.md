# API_CadastroDeEscolas
<p>Aplicação de registo de escola - Desafio back-end</p>
Esta é uma aplicação back-end, (API), feita na versão do .NET CORE 7.0 e CQRS como Padrão Arquitectural
<h6>Funcionalidades: </h6>
<ul> 
   <li>Criar novas escola</li>
   <li>Editar escola</li>
   <li>Eliminar escola</li>
   <li>Listar todas escolas cadastradas</li>
   <li>Filtrar uma escola especifica</li>
   <li>Importar dados do excel para base de dados</li>	
</ul>

<h6>Tecnologias Utilizadas:</h6>
<ul> 
   <li>.NET CORE 7.0</li>
   <li>Linguagem de Programação C#</li>
   <li>Dapper</li>
   <li>CQRS como Padrão Arquitectural</li>
   <li>SGB SQL Server 2019, para criação do banco de dados</li>
</ul>

<h6>Para Execução do projecto:</h6>

<p>Modelo de planilha excel a ser importado:</p>
<p> A imagem abaixo ilustra a estrutura do campo em excel na qual devem estar para que a importação funciona</p>

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





