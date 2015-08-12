USE [CkeTimePollData]

SELECT[SSN]
	  ,ejc.JobCode
	  ,MIN(tf.FileDate) BeginDate
	  ,MAX(tf.FileDate) EndDate
	  ,COUNT(*) TimeCardCount
  FROM [dbo].[Employee] emp
  INNER JOIN EmployeeJobCode ejc ON emp.EmployeeId = ejc.EmployeeId
  INNER JOIN TimeFile tf ON ejc.TimeFileId = tf.TimeFileId
  WHERE ejc.JobCode = '11003'
  GROUP BY [SSN],  ejc.JobCode
  ORDER BY SSN
	  