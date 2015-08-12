INSERT INTO [Detail].[OrderItem]
           ([OrderItemId]
		   ,[OrderID]
           ,[MenuItemID]
           ,[Quantity]
           ,[OrderItemType]
           ,[Price]
           ,[Cost]
           ,[DiscountAmount])
     VALUES
           (!ORDERITEMID
           ,!ORDERID
           ,!MENUITEMID
           ,!QUANTITY
           ,!ORDERITEMTYPE
           ,!PRICE
           ,!COST
           ,!DISCOUNTAMOUNT)

