This program was made with sqlserver.

So for using this in your pc, is need to create a table and chance the connection string on the project. 

There's a connection string in "Model/AlunosDB.cs" (In the Connect() function)


CREATE TABLE Alunos
(
	Id int primary key, 
	nm_Nome nvarchar(50),
	ds_Email nvarchar(50),
	img_Imagem varbinary(MAX)
)

I know some names are out of pattern, It was made as a test.

For finding the connection string in a easy way go "Tools > Connect with a Database" and put the name of your SQL Server when you connect it