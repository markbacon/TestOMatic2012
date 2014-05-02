SELECT [SSN]
      ,[ClockInOutTime]
      ,[InOrOut]
      ,[IsAdjusted]
      ,[CreateDate]
  FROM [HFSDB_Clock].[Staging].[LegacyTimeClock]
  WHERE ClockInOutTime < '2014-04-28'
  ORDER BY SSN, ClockInOutTime, InOrOut



