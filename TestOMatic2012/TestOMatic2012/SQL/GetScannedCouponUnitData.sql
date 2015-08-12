DECLARE @HereAmounts TABLE
(
	BusinessDate DATE,
	HereAmount DECIMAL(18, 2),
	HereCount INT
)

DECLARE @DriveThruAmounts TABLE
(
	BusinessDate DATE,
	DriveThruAmount DECIMAL(18, 2),
	DriveThruCount INT
)

DECLARE @ToGoAmounts TABLE
(
	BusinessDate DATE,
	ToGoAmount DECIMAL(18, 2),
	ToGoCount INT
)

INSERT INTO @HereAmounts (BusinessDate, HereAmount, HereCount)
SELECT	CONVERT(DATE, TransactionTime)
	   ,ISNULL(SUM([Price]), 0)
	   ,ISNULL(SUM([Quantity]), 0)
FROM [dbo].[ScannedCoupon]
WHERE [RestaurantNumber] = '!RESTAURANT_NO'
AND Destination LIKE 'Here%'
AND TransactionTime >= '2014-10-06'
GROUP BY CONVERT(DATE, TransactionTime)
ORDER BY CONVERT(DATE, TransactionTime)



INSERT INTO @DriveThruAmounts (BusinessDate, DriveThruAmount, DriveThruCount)
SELECT	CONVERT(DATE, TransactionTime)
	   ,ISNULL(SUM([Price]), 0)
	   ,ISNULL(SUM([Quantity]), 0)
FROM [dbo].[ScannedCoupon]
WHERE [RestaurantNumber] = '!RESTAURANT_NO'
AND Destination LIKE 'Drive%'
AND TransactionTime >= '2014-10-06'
GROUP BY CONVERT(DATE, TransactionTime)
ORDER BY CONVERT(DATE, TransactionTime)

INSERT INTO @ToGoAmounts (BusinessDate, ToGoAmount, ToGoCount)
SELECT	CONVERT(DATE, TransactionTime)
	   ,ISNULL(SUM([Price]), 0)
	   ,ISNULL(SUM([Quantity]), 0)
FROM [dbo].[ScannedCoupon]
WHERE [RestaurantNumber] = '!RESTAURANT_NO'
AND Destination LIKE 'To%'
AND TransactionTime >= '2014-10-06'
GROUP BY CONVERT(DATE, TransactionTime)
ORDER BY CONVERT(DATE, TransactionTime)


SELECT	 CONVERT(DATE, sc.TransactionTime) AS BusinessDate
		,ISNULL(HereAmount, 0) AS HereAmount
		,ISNULL(HereCount, 0) AS HereCount
		,ISNULL(ToGoAmount, 0) AS ToGoAmount
		,ISNULL(ToGoCount, 0) AS ToGoCount
		,ISNULL(DriveThruAmount, 0) AS DriveThruAmount
		,ISNULL(DriveThruCount, 0) AS DriveThruCount
FROM [dbo].[ScannedCoupon] sc
LEFT JOIN @HereAmounts ha ON CONVERT(DATE, sc.TransactionTime) = ha.BusinessDate
LEFT JOIN @ToGoAmounts ta ON CONVERT(DATE, sc.TransactionTime) = ta.BusinessDate
LEFT JOIN @DriveThruAmounts dta ON CONVERT(DATE, sc.TransactionTime) = dta.BusinessDate
WHERE [RestaurantNumber] = '!RESTAURANT_NO'
AND TransactionTime >= '2014-10-06'

GROUP BY CONVERT(DATE, sc.TransactionTime)
		,ISNULL(HereAmount, 0)
		,ISNULL(HereCount, 0)
		,ISNULL(ToGoAmount, 0)
		,ISNULL(ToGoCount, 0)
		,ISNULL(DriveThruAmount, 0)
		,ISNULL(DriveThruCount, 0)

ORDER BY CONVERT(DATE, TransactionTime)










