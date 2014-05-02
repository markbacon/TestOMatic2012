--DECLARE @OrderId INT

SET @OrderId = !ORDER_ID

PRINT @OrderId

DELETE  FROM [Detail].[OrderItemModifier]
WHERE OrderItemID IN
(
	SELECT OrderItemId
	FROM Detail.OrderItem
	WHERE OrderID = @OrderId
)

DELETE  FROM [Detail].[OrderItem]
WHERE OrderID = @OrderId


DELETE  FROM [Detail].[POSOrder]
WHERE OrderID = @OrderId
