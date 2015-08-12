USE [INFO2000]

DECLARE @CouponId INT

SELECT @CouponId = [coupon_id]
  FROM [dbo].[coupon_dim]
  WHERE coupon_type = 'SCANNED'
  AND [restaurant_no] = !RESTAURANT_NUMBER


IF @CouponId IS NULL
BEGIN

	INSERT INTO [dbo].[coupon_dim]
			   ([restaurant_no]
			   ,[System_id]
			   ,[coupon_class]
			   ,[coupon_type]
			   ,[coupon_rate]
			   ,[coupon_descr]
			   ,[eff_date]
			   ,[term_date])
		 VALUES
			   (!RESTAURANT_NUMBER
			   ,2
			   ,'store'
			   ,'SCANNED'
			   ,NULL
			   ,'Scanned Coupon'
			   ,'1900-01-01'
			   ,NULL)


	SET @CouponId = SCOPE_IDENTITY()

END

SELECT @CouponId AS coupon_id

