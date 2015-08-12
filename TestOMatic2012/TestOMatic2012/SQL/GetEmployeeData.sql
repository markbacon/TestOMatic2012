USE [TimeFileAnalysis]

SELECT [EmployeeName]
      ,[SSN]
  FROM [dbo].[TimeCardRecord]
GROUP BY EmployeeName, SSN
ORDER BY EmployeeName





