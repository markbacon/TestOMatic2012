USE [HardeesTransactionData]

SELECT thf.UnitNumber
	  ,oi.[ItemNumber]
      ,oi.[ItemName]
      ,SUM([Quantity]) AS Quantity
  FROM [dbo].[OrderItem] oi
  INNER JOIN StoreTransaction st ON oi.StoreTransactionId = st.StoreTransactionId
  INNER JOIN TransHistFile thf ON st.TransHistFileId = thf.TransHistFileId

WHERE ItemNumber IN ( '30947','32049','32050','32051','32052','32053','32054','32055','32056','32057',
					  '32058','32059','32060','32061','32062','32063','32064','32065','32066','32067',
					  '32068','32069','32070','32071','32072','32073','32074','32075','32076','32077')

AND CONVERT(DATE, st.TransactionDate) BETWEEN '2015-08-04' AND '2015-08-10'


GROUP BY thf.UnitNumber, oi.[ItemNumber], oi.[ItemName]
ORDER BY thf.UnitNumber, oi.[ItemNumber], oi.[ItemName]


