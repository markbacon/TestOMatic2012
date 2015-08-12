SELECT [discount_dtl_id]
      ,[discount_id]
      ,[pod_id]
      ,[pos_fact_id]
      ,[system_id]
      ,[restaurant_no]
      ,[cal_date]
      ,[total_amt]
      ,[total_cnt]
      ,[create_date]
      ,[create_by]
      ,[last_chg_date]
      ,[last_chg_by]
      ,[source]
  FROM [dbo].[discount_dtl_fact]
where Restaurant_no = 1100140
AND discount_id = 10649
AND cal_date BETWEEN '2013-08-01' AND '2014-07-31'