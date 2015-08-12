INSERT INTO [Detail].[OrderItemModifier]
           ([OrderItemModifierID]
           ,[OrderItemID]
           ,[MenuItemID]
           ,[ModifierActionID]
           ,[Quantity])
     VALUES
           (!ORDERITEMMODIFIERID
           ,!ORDERITEMID
           ,!MENUITEMID
           ,!MODIFIERACTIONID
           ,!QUANTITY)

