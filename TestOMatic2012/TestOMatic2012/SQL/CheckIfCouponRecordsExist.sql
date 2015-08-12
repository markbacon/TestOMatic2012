SELECT COUNT(*) As CouponRecordCount
FROM [dbo].[coupon_dtl_fact]
WHERE [coupon_id] = !COUPON_ID
AND   [pos_fact_id] = !POS_FACT_ID
AND   [restaurant_no] = !RESTAURANT_NO
AND   [cal_date] = '!CAL_DATE'

