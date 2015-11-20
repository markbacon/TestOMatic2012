SELECT [im_prdno] AS ItemNumber
      ,[im_clas_cd] AS ClassCode
      ,[im_idlconv] AS ActualConversionFactor
      ,[im_actconv] AS IdealConversionFactor
FROM [dbo].[fcprdmst]
WHERE im_active = 1
AND   im_clas_cd < '99'


