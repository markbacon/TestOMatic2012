USE [INFO2000]

DECLARE @CouponId INT,
		@RestaurantNumber INT

SET @RestaurantNumber = CONVERT(INT, '!RESTAURANT_NUMBER')

SELECT @CouponId = [coupon_id]
  FROM [dbo].[coupon_dim]
  WHERE coupon_type = 'SCANNED'
  AND [restaurant_no] = @RestaurantNumber


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
			   (@RestaurantNumber
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

