USE INFO2000


SET IDENTITY_INSERT [dbo].discount_dtl_fact ON


INSERT INTO [dbo].[discount_dtl_fact]
           ( discount_dtl_id
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
           ,[source])
     VALUES
           (!DISCOUNT_DTL_ID
		   ,!DISCOUNT_ID
           ,!POD_ID
           ,!POS_FACT_ID
           ,!SYSTEM_ID
           ,!RESTAURANT_NO
           ,'!CAL_DATE'
		   ,!TOTAL_AMT
           ,!TOTAL_CNT
           ,'!CREATE_DATE'
           ,'!CREATE_BY'
           ,'!LAST_CHG_DATE'
           ,'!LAST_CHG_BY'
           ,'!SOURCE')

SET IDENTITY_INSERT [dbo].discount_dtl_fact OFF




