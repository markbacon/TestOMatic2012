SELECT [pos_fact_id]
  FROM [dbo].[pos_fact]
WHERE [Restaurant_no] = !RESTAURANT_NUMBER
AND [cal_date] = '!BUSINESS_DATE'