--Desenvolvedores
--Arthur Augusto Micheletti de Souza RA: 081180004
--Guilherme Sant’Anna                RA: 081180012
--Hallef Carvalho Alves              RA: 081180014
--Julia Muniz de Oliveira            RA: 081180019
--Kaian Tiago dos Santos Lima        RA: 081180022


----criação do banco
USE master
GO

DROP DATABASE VirtualShelf
GO

CREATE DATABASE VirtualShelf
GO

USE [VirtualShelf]
GO
select * from midias

CREATE TABLE [dbo].[generos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
go
CREATE TABLE [dbo].[tipoMidia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descricao] [varchar](20) NULL,
	CONSTRAINT PK_tipoMidia PRIMARY KEY CLUSTERED (id),
)
go

CREATE TABLE [dbo].[statusMidia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descricao] [varchar](20) NULL,
	CONSTRAINT PK_statusMidia PRIMARY KEY CLUSTERED (id),
)

GO

CREATE TABLE [dbo].[midias](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NULL,
	[autor] [varchar](50) NULL,
	[lancamento] [varchar](50) NULL,
	[descricao] [varchar](50) NULL,
	[imagem] [varbinary](max) NULL,
	[generoId] [int] NULL,
	[tipoMidiaId] [int] NOT NULL,
	CONSTRAINT PK_midias PRIMARY KEY CLUSTERED (id),
	CONSTRAINT FK_tipoMidia FOREIGN KEY (tipoMidiaId) REFERENCES tipoMidia(id),
	CONSTRAINT FK_genero FOREIGN KEY (generoId) REFERENCES generos(id)
)
GO

CREATE TABLE usuarios (
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](50) NULL,
	[senha] [varchar](50) NULL,
	[nome] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[telefone] [varchar](11) NULL,
	[ehPrivado] [bit] NULL,
	CONSTRAINT PK_usuarios PRIMARY KEY CLUSTERED (id),
)
GO


CREATE TABLE [dbo].[midiasUsuarios](
	[usuarioId] [int] NOT NULL,	
	[statusMidiaId] [int] NULL,
	[dataAdd] [date] NULL,
	[midiaId] [int] NOT NULL,
	CONSTRAINT PK_MidiaUsuarios PRIMARY KEY (usuarioId,midiaId),
	CONSTRAINT FK_midias FOREIGN KEY ([midiaId]) REFERENCES Midias(id),
	CONSTRAINT FK_usuarios FOREIGN KEY (usuarioId) REFERENCES usuarios(id),
	CONSTRAINT FK_statusMidia FOREIGN KEY ([statusMidiaId]) REFERENCES statusMidia(id)
)
GO



CREATE TABLE statusAmizade(
    [id] [int] IDENTITY(1,1) NOT NULL,
	[descricao] [varchar](20) NULL,
	CONSTRAINT PK_statusAmizades PRIMARY KEY CLUSTERED (id)
)
GO


CREATE TABLE amizades(
	[id] [int] NOT NULL,
	[amigoId] [int] NOT NULL,
	[data] [date] NULL,
	[statusId] [int] NOT NULL,
	CONSTRAINT PK_amizades PRIMARY KEY CLUSTERED (id, amigoId),
	CONSTRAINT FK_amizades FOREIGN KEY (amigoId) REFERENCES usuarios(id),
	CONSTRAINT FK_amizades2 FOREIGN KEY (id) REFERENCES usuarios(id),
	CONSTRAINT FK_statusAmizades FOREIGN KEY ([statusId]) REFERENCES statusAmizade(id)
)






--------------Stored Procedures

create PROCEDURE [dbo].[spListaMidiaPeloTipo](@tipoMidiaId int, @userId int)
As
Begin

	select * from midias m where tipoMidiaId = @tipoMidiaId

end
go


CREATE PROCEDURE [dbo].[spListaTop] (
    @tipoMidiaId int)
AS
begin
	select top 1 * from midias where @tipoMidiaId = tipoMidiaId
end
go




create procedure [dbo].[spInsereMidiaUsuario](@usuarioId int,@midiaId int, @statusMidiaId int)
as
begin
	insert into MidiasUsuarios (usuarioId,midiaId,statusMidiaId,dataAdd)  
		values(@usuarioId ,@midiaId , @statusMidiaId,GETDATE())
end
GO





create procedure [dbo].[spRemoveMidiaUsuario](@usuarioId int,@midiaId int)
as
begin
	delete from MidiasUsuarios where @usuarioId = MidiasUsuarios.usuarioId and 
		MidiasUsuarios.midiaId = @midiaId
end
go






go
create procedure [dbo].[spConsultaComposta](@idUsuario int , @idCategoria int,@idTipo int, @nome varchar(max))
as
begin
	

	declare @sql varchar(max)
   set @sql = 'select u.nome "nome", t.descricao "tipo",m.nome "titulo",
			g.nome "categoria", m.id "mdId", sm.descricao "status"
			from midiasUsuarios mu inner join midias m on mu.midiaId = m.id
			inner join generos g on m.generoId = g.id
			inner join tipoMidia t on t.id= m.tipoMidiaId
			inner join usuarios u on u.id= mu.usuarioId
			inner join statusMidia sm on sm.id = mu.statusMidiaId where '+cast(@idUsuario as varchar(max))+' = mu.usuarioId ';
	
	if(@nome is not null)
		set @sql = @sql + 'and m.nome like ''%' + @nome + '%''';

	if (@idCategoria is not null)
       set @sql = @sql + ' and m.generoId = '+ cast(@idCategoria as varchar(max));
    
	if (@idTipo is not null)
        set @sql = @sql +  ' and  t.id  =  '+ cast(@idTipo as varchar(max));
		

    set @sql  = @sql  + ' order by mu.statusMidiaId, m.nome ';
 

    exec(@sql);

end




go
create procedure [dbo].[spInsert_midias](
@id int,
    @nome varchar(50),
    @produtora varchar(50),
    @lancamento varchar(50),
    @descricao varchar(50),
    @imagem varbinary(max),
    @tipoMidiaId int,
    @generoId int 
)
as
begin
select * from midias
 insert into midias 
 (nome,
     autor,
    lancamento ,
    descricao ,
    imagem ,
    generoId, 
	tipoMidiaId)
    values
    (@nome,
    @produtora ,
    @lancamento ,
    @descricao ,
    @imagem ,
    @generoId,
	@tipoMidiaId)
end
go




create procedure [dbo].[spInsert_usuarios]
(
@id int,
    @nome varchar(50),
    @login varchar(50),
    @senha varchar(50),
    @email varchar(50),
    @telefone varchar(50),
    @ehPrivado bit
)
as
begin
 insert into usuarios
  (nome,
    [login] ,
    senha ,
    email ,
    telefone ,
    ehPrivado)
    values
    (@nome,
    @login ,
    @senha ,
    @email ,
    @telefone ,
    @ehPrivado)
end
go




create procedure [dbo].[spUpdate_midias](
@id int,
    @nome varchar(50),
    @produtora varchar(50),
    @lancamento varchar(50),
    @descricao varchar(50),
    @imagem varbinary(max),
    @generoId int,
	@tipoMidiaId int 
)

as
begin
 update midias set
 nome = @nome,
 autor = @produtora,
 lancamento = @lancamento,
 descricao = @descricao,
 imagem = @imagem,
 generoId = @generoId,
 tipoMidiaId = @tipoMidiaId
 where id = @id
end
GO




create procedure [dbo].[spUpdate_usuarios](
@id int,
    @nome varchar(50),
    @login varchar(50),
    @senha varchar(50),
    @email varchar(50),
    @telefone varchar(50),
    @ehPrivado bit
)

as
begin
 update usuarios set
 [login] = @login,
 senha = @senha,
 nome = @nome,
 email= @email,
 telefone = @telefone,
 ehPrivado = @ehPrivado
 where id = @id
end
GO



create procedure [dbo].[spDelete]
(
 @id int ,
 @tabela varchar(max)
)
as
begin
 declare @sql varchar(max);
 set @sql = ' delete ' + @tabela +
 ' where id = ' + cast(@id as varchar(max))
 exec(@sql)
end
go





create procedure [dbo].[spConsulta]
(
 @id int ,
 @tabela varchar(max)
)
as
begin
 declare @sql varchar(max);
 set @sql = 'select * from ' + @tabela +
 ' where id = ' + cast(@id as varchar(max))
 exec(@sql)
end
GO




create procedure [dbo].[spProximoId]
(@tabela varchar(max))
as
begin
 exec('select isnull(max(id) +1, 1) as MAIOR from '
 +@tabela)
end
go




create procedure [dbo].[spListagem]
(
 @tabela varchar(max),
 @ordem varchar(max))
as
begin
 exec('select * from ' + @tabela +
 ' order by ' + @ordem)
end
go





create procedure [dbo].[spListaFriends](@id varchar(max), @pedido bit)
as
begin
	if @pedido = 0
	begin
		select u.* from amizades s 
		inner join usuarios u on s.amigoId = u.id 
		where s.id = @id and s.statusId = 1
		union
		select u.* from amizades s 
		inner join usuarios u on s.amigoId = @id
		where s.id = u.id and s.statusId = 1
	end
	else
	begin
		select u.* from amizades s 
		inner join usuarios u on s.amigoId = @id
		where s.id = u.id and s.statusId = 2
	end
	
end
go





create procedure [dbo].[spSolicitaAmizade](@usuarioId int,@friendId int)
as
begin
	insert into amizades (id,amigoId, statusId, data)  values(@usuarioId ,@friendId, 2, GETDATE())
end





go
create procedure [dbo].[spCancelaAmizade](@usuarioId int,@friendId int)
as
begin
	delete amizades where (amigoId = @usuarioId and id = @friendId) or
	(id = @usuarioId and amigoId = @friendId)
end





go
create procedure [dbo].[spConcretizaAmizade](@usuarioId int,@friendId int)
as
begin
	update amizades set statusId = 1 where id = @friendId and amigoId = @usuarioId
end
go




create procedure [dbo].[spValidaLogin](
	@login varchar(50),
	@senha varchar(50)
)
as
begin
	select * from usuarios u where [login] = @login and senha = @senha
end
GO




CREATE PROCEDURE [dbo].[spBuscarUser] (
    @usuarios VARCHAR(MAX), 
    @midias VARCHAR(MAX) )
AS
    IF(@usuarios IS NOT NULL)
        BEGIN
        SELECT * FROM buscarUsuarioNome(@usuarios)
        END
    IF(@midias IS NOT NULL)
        BEGIN 
        SELECT * FROM buscarUsuarioMidia(@midias)
        END
go



--------------------functions
CREATE FUNCTION [dbo].[buscarUsuarioNome](@usuario VARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM usuarios WHERE ([login] LIKE '%' + @usuario + '%' 
    OR nome LIKE '%' + @usuario + '%') and ehPrivado=0
);
GO





CREATE FUNCTION [dbo].[buscarUsuarioMidia](@midia VARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT us.* FROM usuarios us INNER JOIN MidiasUsuarios mid ON us.id = mid.usuarioId
    INNER JOIN midias m ON m.id = mid.midiaId 
    WHERE (m.nome LIKE '%' + @midia + '%' OR m.autor LIKE '%' + @midia + '%')
	and ehPrivado=0
);
GO







-----------------------------------triggers
create TRIGGER [dbo].[trgDeleta_midias]
    ON [dbo].[midias]
    INSTEAD OF DELETE AS
BEGIN
    DECLARE @id INT
    SET @id = (SELECT id FROM deleted)

        DECLARE cursorDeletaMidias CURSOR STATIC FOR
            SELECT midiaId FROM midiasUsuarios WHERE midiaId = @id

        DECLARE @midiaId INT

        OPEN cursorDeletaMidias 

        FETCH NEXT FROM cursorDeletaMidias 
            INTO @midiaId 

        WHILE (@@FETCH_STATUS = 0 OR @@FETCH_STATUS = -2)
        BEGIN
            IF (@@FETCH_STATUS = 0)
            BEGIN
                IF (@midiaId IS NOT NULL)
                BEGIN
                    DELETE FROM midiasUsuarios WHERE @midiaId = midiaId
                END
            END

        FETCH NEXT FROM cursorDeletaMidias 
            INTO @midiaId 
        END

        CLOSE cursorDeletaMidias 
        DEALLOCATE cursorDeletaMidias 
    DELETE FROM midias WHERE @midiaId = id
END
go



create TRIGGER [dbo].[trgDeleta_Usuarios]
    ON [dbo].[usuarios]
    INSTEAD OF DELETE AS
BEGIN
    DECLARE @id INT
    SET @id = (SELECT id FROM deleted)
	
	delete from amizades where amigoId = @id or id = @id
	
	delete from midiasUsuarios where usuarioId = @id
    
    DELETE FROM usuarios WHERE id = @id
END
go



