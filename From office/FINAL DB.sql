CREATE TABLE Books (

		[Id]		   INT IDENTITY (1,1) PRIMARY KEY,
		[Title]		   NVARCHAR (100),
		[Author]	   NVARCHAR (100),
		[Description]  NVARCHAR (MAX),			-- Unicode characters upto 2 GB --
		[ImageUrl]	   NVARCHAR (255),
		[Price]		   DECIMAL	(18,2),
		[Availability] NVARCHAR (50)

);

/*
ALTER TABLE Books
ALTER COLUMN [ImageUrl] NVARCHAR(MAX);		-- Set the length of Image Url Column --

UPDATE Books SET [Price] = 130.77 WHERE [Id] = 6;
UPDATE Books SET [Price] = 60.87  WHERE [Id] = 7;
UPDATE Books SET [Price] = 99.56  WHERE [Id] = 8;
UPDATE Books SET [Price] = 130.77 WHERE [Id] = 9;
UPDATE Books SET [Price] = 100.47 WHERE [Id] = 10;
SELECT * FROM Books;
SELECT * FROM ContactForm;
DELETE FROM Books WHERE Id = 12;
DELETE FROM ContactForm WHERE Id = 4;

*/ 

SELECT * FROM Books;
INSERT INTO Books([Title], [Author], [Description], [ImageUrl], [Price], [Availability])
VALUES ('Book 1','Author 1', 'Description 1', 'https://m.media-amazon.com/images/I/81q77Q39nEL._AC_UF1000,1000_QL80_.jpg', 10.99, 'In Stock'),
	   ('Book 2','Author 2', 'Description 2', 'https://marketplace.canva.com/EAFaQMYuZbo/1/0/1003w/canva-brown-rusty-mystery-novel-book-cover-hG1QhA7BiBU.jpg', 10.99, 'Out Of Stock'),
	   ('Book 3','Author 3', 'Description 3', 'https://images-us.bookshop.org/ingram/9781250822055.jpg?height=500&v=v2-775a9b07940f44786ff5ec071ff42da3.jpeg', 10.99, 'In Stock'),
	   ('Book 4','Author 4', 'Description 4', 'https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEgwUFEsq8iUmF26DsoKqCqceFO44Py_5-0rwS1LT5ep4flHnas3-8JULNhAWzt9qfYO1x38tKPVBc4yeG8NLC9D6xJvLj3qtKXLnn6QTi5kAO-0uLOasBLpaXZw22z4Jo3qxLSPl0vZtbRE_35X1RduZBRIpQYRWBbdTNeuKhaZlFSJiLfm7CDKWvY5ZQ/s2362/WHEN%20THE%20SMOKE%20CLEARED%20BOOK%20COVER.jpg', 10.99, 'Out Of Stock'),
	   ('Book 5','Author 5', 'Description 5', 'https://www.editorialdepartment.com/wp-content/uploads/2015/04/Book-Title.jpg', 10.99, 'In Stock'),
	   ('Book 6','Author 6', 'Description 6', 'https://skullsinthestars.com/wp-content/uploads/2021/10/howcanhekeepstandinglikethat.jpg', 10.99, 'Out Of Stock');

CREATE TABLE ContactForm(
	
		[Id]      INT IDENTITY (1,1) PRIMARY KEY,
		[Name]    NVARCHAR (100),
		[Email]	  NVARCHAR (100),
		[Subject] NVARCHAR (100),
		[Message] NVARCHAR (MAX)					-- Unicode characters upto 2 GB --

);


-----------------------------------------------------STORED PROCEDURE FOR BOOKS--------------------------------------------------------------------------------


USE [5619]
GO



----------GET BOOKS----------

CREATE PROCEDURE GetAllBooks
AS
BEGIN
    SELECT * FROM Books
END
GO

----------GET BOOKS BY ID----------


CREATE PROCEDURE GetBooksById
    @Id INT 
AS
BEGIN
    SELECT * FROM Books WHERE Id = @Id
END
GO

----------INSERTING BOOKS----------


CREATE PROCEDURE AddBooks

		@Title		  NVARCHAR (100),
		@Author		  NVARCHAR (100),
		@Description  NVARCHAR (MAX),
		@ImageUrl	  NVARCHAR (255),
		@Price		  DECIMAL  (18, 2),
		@Availability NVARCHAR (50)

AS
BEGIN
    INSERT INTO Books(Title, Author, [Description], ImageUrl, Price, [Availability])
    VALUES (@Title, @Author, @Description, @ImageUrl, @Price, @Availability)
END
GO

----------UPDATE BOOKS----------

CREATE PROCEDURE UpdateBooks

		@Id			  INT,
		@Title		  NVARCHAR (100),
		@Author		  NVARCHAR (100),
		@Description  NVARCHAR (MAX),
		@ImageUrl	  NVARCHAR (255),
		@Price		  DECIMAL  (18, 2),
		@Availability NVARCHAR (50)
AS
BEGIN
    UPDATE Books
    SET Title	        =  @Title,
		Author	        =  @Author,
        [Description]   =  @Description,
		ImageUrl		=  @ImageUrl,
        Price			=  @Price,
        [Availability]	=  @Availability

    WHERE Id = @Id
END
GO

----------DELETE BOOKS----------

CREATE PROCEDURE DeleteBooks
    @Id INT
AS
BEGIN
    DELETE FROM Books WHERE Id = @Id
END
GO


-----------------------------------------------------STORED PROCEDURE FOR CONTACT FORM--------------------------------------------------------------------------------

USE [5619]
GO



----------GET USER COMMENTS----------

CREATE PROCEDURE GetAllComments
AS
BEGIN
    SELECT * FROM ContactForm
END
GO

----------GET COMMENTS BY ID----------


CREATE PROCEDURE GetCommentsById
    @Id INT 
AS
BEGIN
    SELECT * FROM ContactForm WHERE Id = @Id
END
GO

----------INSERTING COMMENTS----------


CREATE PROCEDURE AddComments

    @Name     NVARCHAR (100),
	@Email	  NVARCHAR (100),
	@Subject  NVARCHAR (100),
	@Message  NVARCHAR (MAX)

AS
BEGIN
    INSERT INTO ContactForm([Name], Email, [Subject], [Message])
    VALUES (@Name, @Email, @Subject, @Message)
END
GO

----------UPDATE COMMENTS----------

CREATE PROCEDURE UpdateComments

     @Id	   INT,
	 @Name     NVARCHAR (100),
	 @Email	   NVARCHAR (100),
	 @Subject  NVARCHAR (100),
	 @Message  NVARCHAR (MAX)
AS
BEGIN
    UPDATE ContactForm

    SET [Name]	    =  @Name,   
		Email	    =  @Email,	 
        [Subject]	=  @Subject,
		[Message]	=  @Message
      

    WHERE Id = @Id
END
GO

----------DELETE COMMENTS----------

CREATE PROCEDURE DeleteComments
    @Id INT
AS
BEGIN
    DELETE FROM ContactForm WHERE Id = @Id
END
GO


CREATE PROCEDURE GetFeaturedBooks
AS
BEGIN
    -- Select the relevant columns from the Books table
    SELECT 
        Title,
        ImageUrl,
        Price,
        Author
    FROM Books
END
