USE [HFSDB]

SELECT [OrderItemID]
      ,[OrderID]
      ,[MenuItemID]
      ,[Quantity]
      ,[OrderItemType]
      ,[Price]
      ,[Cost]
      ,[DiscountAmount]
  FROM [Detail].[OrderItem]
  WHERE OrderID IN
  (
	SELECT OrderID
	FROM [Detail].[POSOrder]
	WHERE Convert(DATE, OrderDate) = '!BUSINESS_DATE'
  )




