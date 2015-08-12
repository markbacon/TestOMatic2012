SELECT	DISTINCT CONVERT(DATE, TransactionTime) AS BusinessDate
FROM ScannedCoupon
WHERE RestaurantNumber = !RESTAURANT_NO
ORDER BY BusinessDate
