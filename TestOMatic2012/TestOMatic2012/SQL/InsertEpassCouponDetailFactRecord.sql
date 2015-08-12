USE [INFO2000]

INSERT INTO [dbo].[coupon_dtl_fact]
           ([coupon_id]
           ,[pos_fact_id]
           ,[system_id]
           ,[restaurant_no]
           ,[cal_date]
           ,[pod_id]
           ,[total_amt]
           ,[total_cnt]
           ,[create_date]
           ,[create_by]
           ,[last_chg_date]
           ,[last_chg_by]
           ,[source])
     VALUES
           (!COUPON_ID
           ,!POS_FACT_ID
           ,2
           ,!RESTAURANT_NO
           ,'!CAL_DATE'
           ,!POD_ID
           ,!TOTAL_AMT
           ,!TOTAL_CNT
           ,GETDATE()
           ,'ckrcorp\mbacon'
           ,GETDATE()
           ,'ckrcorp\mbacon'
           ,'M')



