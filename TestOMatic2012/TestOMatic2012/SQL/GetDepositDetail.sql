USE [DataAnalysis]


SELECT[UnitNumber]
      ,[DepositDate]
      ,[Amount]
      ,[Status]
      ,[HFSDB_DepositId]
  FROM [dbo].[DepositDetail]
WHERE UnitNumber = 'X!UNIT_NUMBER'
AND DepositDate = '!DEPOSIT_DATE'
AND Status != 'Deleted'


