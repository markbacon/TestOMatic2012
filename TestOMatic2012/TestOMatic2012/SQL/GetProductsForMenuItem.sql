SELECT [jp_year]
      ,[jp_per]
      ,[jp_parnt]
      ,[jp_seqn]
      ,[jp_child]
      ,[jp_psreq]
      ,[jp_psyld]
      ,[jp_yldsv]
  FROM [dbo].[fcpssjp]
  WHERE jp_parnt = '!MENU_ITEM_ID'
  AND jp_year = 2015
  AND jp_per = 9
