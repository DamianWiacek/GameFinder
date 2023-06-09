# GameFinder
Application created to help find games in your city.


# Tech stack
- .NET 7
- SqlServer
- MediatR
- FluentValidation

# Database Structure


![image](https://github.com/DamianWiacek/GameFinder/assets/109426665/b42c71fa-26f4-48c6-9057-df6df888a087)


```
CREATE TRIGGER trg_Welcome_User_Mail
ON [User]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[EMail]
           ([UserEmailAddress]
           ,[Subject]
           ,[Message]
           ,[IsSent])
		   select i.Email, 'Greetings from GameFinder Team', 'Welcome to GameFinder, we hope you will find lot of games here!',0
    
    FROM inserted i;
    
    
    
    
    
    SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE p_RemoveSentEmails

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete EMail where IsSent = 1
END
GO


CREATE FUNCTION dbo.f_PeselValidate (@pesel VARCHAR(11))
RETURNS BIT
AS
BEGIN
    DECLARE @result BIT;
    DECLARE @sum INT = 0;

    IF LEN(@pesel) <> 11
    BEGIN
        SET @result = 0;
    END
    ELSE
    BEGIN
        SET @sum =
            CAST(SUBSTRING(@pesel, 1, 1) AS INT) * 1 +
            CAST(SUBSTRING(@pesel, 2, 1) AS INT) * 3 +
            CAST(SUBSTRING(@pesel, 3, 1) AS INT) * 7 +
            CAST(SUBSTRING(@pesel, 4, 1) AS INT) * 9 +
            CAST(SUBSTRING(@pesel, 5, 1) AS INT) * 1 +
            CAST(SUBSTRING(@pesel, 6, 1) AS INT) * 3 +
            CAST(SUBSTRING(@pesel, 7, 1) AS INT) * 7 +
            CAST(SUBSTRING(@pesel, 8, 1) AS INT) * 9 +
            CAST(SUBSTRING(@pesel, 9, 1) AS INT) * 1 +
            CAST(SUBSTRING(@pesel, 10, 1) AS INT) * 3;

        SET @sum = @sum % 10;
        IF @sum = 0
        BEGIN
            SET @sum = 10;
        END;

        SET @result = CASE WHEN (10 - @sum) = CAST(SUBSTRING(@pesel, 11, 1) AS INT) THEN 1 ELSE 0 END;
    END;

    RETURN @result;
END;



CREATE FUNCTION dbo.f_ValidateDateOfBirth (@dateOfBirth DATE)
RETURNS BIT
AS
BEGIN
    DECLARE @currentDate DATE = GETDATE();
    DECLARE @minimumAge INT = 8; -- Minimalny wiek (lata)
    DECLARE @maximumAge INT = 120; -- Maksymalny wiek (lata)

    IF @dateOfBirth IS NULL
        RETURN 0;

    IF @dateOfBirth > DATEADD(YEAR, -@minimumAge, @currentDate)
        RETURN 0;

    IF @dateOfBirth < DATEADD(YEAR, -@maximumAge, @currentDate)
        RETURN 0;

    RETURN 1;
END;


CREATE TRIGGER tr_ValidateUser
ON dbo.użytkownicy
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @userId INT;
    DECLARE @dateOfBirth DATE;
    DECLARE @pesel VARCHAR(11);

    SELECT @userId = inserted.Id,
           @dateOfBirth = inserted.data_urodzenia,
           @pesel = inserted.pesel
    FROM inserted;

    -- Sprawdzenie daty urodzenia
    IF dbo.f_ValidateDateOfBirth(@dateOfBirth) = 0
    BEGIN
        RAISERROR ('Nieprawidłowa data urodzenia', 16, 1);
        DELETE FROM dbo.użytkownicy WHERE Id = @userId;
        RETURN;
    END;

    -- Sprawdzenie numeru PESEL
    IF dbo.f_PeselValidate(@pesel) = 0
    BEGIN
        RAISERROR ('Nieprawidłowy numer PESEL', 16, 1);
        DELETE FROM dbo.użytkownicy WHERE Id = @userId;
        RETURN;
    END;
END;
```
# Autorzy
- Damian Wiącek
- Oliwier Wojewoda
