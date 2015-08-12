USE [CkeTimePollData]

SELECT [EmployeeId]
      ,[EmployeeName]
      ,[LastName]
      ,[FirstName]
      ,[SSN]
  FROM [dbo].[Employee]
WHERE SSN = '!SSN'
ORDER BY EmployeeId DESC
