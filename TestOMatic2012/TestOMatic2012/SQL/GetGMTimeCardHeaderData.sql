USE [CkeTimePollData]

SELECT tf.[FileName]
      ,tf.[UnitNumber]
      ,tf.[FileDate]
	  ,ejc.EmployeeId
	  ,ejc.JobCode
	  ,e.EmployeeName
	  ,e.SSN
  FROM [dbo].[TimeFile] tf
  INNER JOIN EmployeeJobCode ejc ON tf.TimeFileId = ejc.TimeFileId
  INNER JOIN Employee e ON ejc.EmployeeId = e.EmployeeId
  WHERE tf.FileDate BETWEEN '2015-03-31' AND '2015-10-06'
  AND ejc.JobCode = '11003'
  ORDER BY UnitNumber, FileDate 


