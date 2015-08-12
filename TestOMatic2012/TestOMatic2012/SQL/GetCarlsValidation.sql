USE [INFO2000]

--DECLARE



SELECT	 pos.restaurant_no
		,pos.cal_date
		,pos.total_net_sales_amt AS TotalNetSales
		,pos.total_net_sales_cnt AS TotalTransactionCount
		,ISNULL(Breakfast.NetSales, 0) as BreakfastNetSales
		,ISNULL(Breakfast.TransactionCount, 0) as BreakfastTransactionCount
		,ISNULL(Lunch.NetSales, 0) as LunchNetSales
		,ISNULL(Lunch.TransactionCount, 0) as LunchTransactionCount
		,ISNULL(Dinner.NetSales, 0) AS DinnerNetSales
		,ISNULL(Dinner.TransactionCount, 0) AS DinnerTransactionCount
		,ISNULL(LateNight.NetSales, 0) AS LateNightNetSales
		,ISNULL(LateNight.TransactionCount, 0) AS LateNightTransactionCount
FROM pos_fact pos
LEFT OUTER JOIN
(
	SELECT sls.[restaurant_no]
		  ,sls.[cal_date]
		  ,SUM(sls.[net_sales_amt]) NetSales
		  ,SUM(sls.[transactions])  TransactionCount
	  FROM [dbo].[dp6_sales_fact] sls
	  INNER JOIN dp_grp_dim grp ON sls.dp_grp_id = grp.dp_grp_id
	  WHERE sls.system_id = 1
	  AND sls.dp_grp_id IN (19, 20)
	  GROUP BY restaurant_no, cal_date
) LateNight ON pos.Restaurant_no = LateNight.restaurant_no AND pos.cal_date = LateNight.cal_date

LEFT OUTER JOIN
(
	SELECT sls.[restaurant_no]
		  ,sls.[cal_date]
		  ,SUM(sls.[net_sales_amt]) NetSales
		  ,SUM(sls.[transactions])  TransactionCount
	  FROM [dbo].[dp6_sales_fact] sls
	  INNER JOIN dp_grp_dim grp ON sls.dp_grp_id = grp.dp_grp_id
	  WHERE sls.system_id = 1
	  AND sls.dp_grp_id = 21
	  GROUP BY restaurant_no, cal_date
) Breakfast ON pos.Restaurant_no = Breakfast.restaurant_no AND pos.cal_date = Breakfast.cal_date

LEFT OUTER JOIN
(
	SELECT sls.[restaurant_no]
		  ,sls.[cal_date]
		  ,SUM(sls.[net_sales_amt]) NetSales
		  ,SUM(sls.[transactions])  TransactionCount
	  FROM [dbo].[dp6_sales_fact] sls
	  INNER JOIN dp_grp_dim grp ON sls.dp_grp_id = grp.dp_grp_id
	  WHERE sls.system_id = 1
	  AND sls.dp_grp_id IN (22, 23)
	  GROUP BY restaurant_no, cal_date
) Lunch ON pos.Restaurant_no = Lunch.restaurant_no AND pos.cal_date = Lunch.cal_date


LEFT OUTER JOIN
(
	SELECT sls.[restaurant_no]
		  ,sls.[cal_date]
		  ,SUM(sls.[net_sales_amt]) NetSales
		  ,SUM(sls.[transactions])  TransactionCount
	  FROM [dbo].[dp6_sales_fact] sls
	  INNER JOIN dp_grp_dim grp ON sls.dp_grp_id = grp.dp_grp_id
	  WHERE sls.system_id = 1
	  AND sls.dp_grp_id IN (24, 25)
	  GROUP BY restaurant_no, cal_date
) Dinner ON pos.Restaurant_no = Dinner.restaurant_no AND pos.cal_date = Dinner.cal_date

WHERE pos.system_id = 1
AND pos.cal_date = '!BUSINESS_DATE'
--AND pos.cal_date BETWEEN '2015-02-01' AND '2015-04-14'
ORDER BY pos.restaurant_no, pos.cal_date
  



