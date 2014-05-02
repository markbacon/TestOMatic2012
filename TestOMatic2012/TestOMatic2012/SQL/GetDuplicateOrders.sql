SELECT OrderNumber,
	   CONVERT(DATE, OrderDate) AS BusinessDate,
	   COUNT(*)
  FROM [Detail].[POSOrder]
  GROUP BY OrderNumber, CONVERT(DATE, OrderDate) HAVING COUNT(*) > 1
  ORDER BY CONVERT(DATE, OrderDate)
