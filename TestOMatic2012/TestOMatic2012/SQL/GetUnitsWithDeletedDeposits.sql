USE [DataAnalysis]

SELECT [UnitNumber]
      ,[DepositDate]
	  ,COUNT(*) AS DeletedDepositCount
  FROM [dbo].[DepositDetail]
  WHERE Status = 'Deleted'
  GROUP BY UnitNumber, DepositDate
  ORDER BY UnitNumber, DepositDate


