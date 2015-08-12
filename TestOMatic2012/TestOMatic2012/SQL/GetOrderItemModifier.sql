USE [HFSDB]

SELECT [OrderItemModifierID]
      ,[OrderItemID]
      ,[MenuItemID]
      ,[ModifierActionID]
      ,[Quantity]
  FROM [Detail].[OrderItemModifier]
WHERE OrderItemID IN 
(
	SELECT [OrderItemID]
	FROM [Detail].[OrderItem]
	WHERE OrderID IN
	(
		SELECT OrderID
		FROM [Detail].[POSOrder]
		WHERE Convert(DATE, OrderDate) = '!BUSINESS_DATE'
	)
)


