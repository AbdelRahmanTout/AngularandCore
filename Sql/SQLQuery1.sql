/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[FirstName]
      ,[LastName]
      ,[UserName]
      ,[Email]
      ,[Password]
      ,[Role]
      ,[Token]
  FROM [AuthDb].[dbo].[Users]