USE [INFO2000_test]

SELECT dtl.[deposit_id]
      ,dtl.[POS_fact_id]
      ,dtl.[Restaurant_no]
      ,dtl.[Cal_Date]
      ,dtl.[Deposit_amt]
  FROM [deposit_dtl_fact] dtl
  --INNER JOIN [deposit_dim] dim ON dtl.deposit_id = dim.deposit_id
  WHERE dtl.System_id = 2
  AND dtl.Cal_Date BETWEEN '2015-08-10' AND '2015-09-07'
  --AND dtl.Restaurant_no IN (1100007, 1100025, 1100030, 1100069, 1100087, 1100096, 1100101, 1100104, 1100106,   
		--					1100130, 1100132, 1100140, 1100155, 1100170, 1100194, 1100280, 1100297, 1100390,   
		--					1100429, 1100482, 1100650)
--  AND deposit_group = 'CASH'



