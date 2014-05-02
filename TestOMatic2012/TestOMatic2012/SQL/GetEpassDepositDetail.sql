SELECT dtl.[deposit_dtl_id]
      ,dtl.[deposit_id]
      ,dtl.[POS_fact_id]
      ,dtl.[System_id]
      ,dtl.[Restaurant_no]
      ,dtl.[Cal_Date]
      ,dtl.[Deposit_amt]
      ,dtl.[create_date]
      ,dtl.[create_by]
      ,dtl.[last_chg_date]
      ,dtl.[last_chg_by]
      ,dtl.[source]
  FROM [INFO2000].[dbo].[deposit_dtl_fact] dtl
  INNER JOIN [INFO2000].[dbo].[deposit_dim] dim ON dtl.deposit_id = dim.deposit_id
  WHERE dtl.Cal_Date = '!CAL_DATE'
  AND dtl.Restaurant_no = !RESTAURANT_NO
  AND dim.deposit_descr = 'Cash Deposit'
  



