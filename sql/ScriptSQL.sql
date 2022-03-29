/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[AeroportoDestino]
      ,[AeroportoOrigem]
      ,[AeronaveId]
      ,[HorarioEmbarque]
      ,[HorarioDesembarque]
  FROM [AndreAirLines].[dbo].[Voo]