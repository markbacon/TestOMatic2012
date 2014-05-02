INSERT INTO [Detail].[EmployeeClock]
           ([EmployeeId]
           ,[ClockInOutTime]
           ,[InOrOut]
           ,[IsPosPunch]
           ,[IsAdjusted]
           ,[IsDeleted]
           ,[CreateDate]
           ,[CreatedBy]
           ,[ModifiedBy]
           ,[ModifiedDate])
     VALUES
           (!EmployeeId
           ,'!ClockInOutTime'
           ,'!InOrOut'
           ,0
           ,0
           ,0
           ,GETDATE()
           ,'EmployeeClockDataFix'
           ,NULL
           ,GETDATE())


