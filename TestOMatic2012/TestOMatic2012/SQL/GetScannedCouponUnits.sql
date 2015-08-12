SELECT DISTINCT RestaurantNumber 
FROM ScannedCoupon 
WHERE RestaurantNumber IN
(	
SELECT DISTINCT restaurant_no FROM INFO2000.dbo.pos_fact
)

ORDER BY RestaurantNumber