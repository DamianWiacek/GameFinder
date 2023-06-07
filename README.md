# GameFinder
Application created to help find games in your city.


# Tech stack
- .NET 7
- SqlServer
- MediatR
- FluentValidation

# Database Structure

![erd](https://user-images.githubusercontent.com/109426665/229372834-38826ebc-4e13-40e5-a497-fa600f431c4e.png)

# Do zrobienia

- rankingi i statystki jakies wymyslic, jakies triggery zeby stasiu zadowlony byl
- zrobic jakis panel dla admina usuwanie gier boisko (w sumie po co)

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
```
# Autorzy
-Damian Wiącek
-Oliwier Wojewoda
