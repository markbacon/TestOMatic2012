SELECT dtl.[deposit_id]
      ,pos.[POS_fact_id]
      ,dtl.[System_id]
      ,dtl.[Restaurant_no]
      ,dtl.[Cal_Date]
      ,dtl.[Deposit_amt]
      ,GETDATE() AS [create_date]
      ,'mbacon' AS [create_by]
      ,GETDATE() AS [last_chg_date]
      ,'mbacon' AS [last_chg_by]
      ,'ML' AS [source]
  FROM anasql05.info2000.[dbo].[deposit_dtl_fact] dtl
  --INNER JOIN anasql05.info2000.[dbo].[deposit_dim] dim ON dtl.deposit_id = dim.deposit_id
  inner join info2000_test.dbo.pos_fact POS on DTL.Restaurant_no = pos.Restaurant_no AND dtl.Cal_Date = pos.cal_date
  WHERE dtl.System_id = 2
  AND dtl.Cal_Date BETWEEN '2015-08-10' AND '2015-09-07'
  --AND dtl.Restaurant_no IN (1100007, 1100025, 1100030, 1100069, 1100087, 1100096, 1100101, 1100104, 1100106,   
		--					1100130, 1100132, 1100140, 1100155, 1100170, 1100194, 1100280, 1100297, 1100390,   
		--					1100429, 1100482, 1100650)
  --AND deposit_group = 'CASH'


