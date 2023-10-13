USE [SP.Customer.DB];
GO
 
DECLARE @subscriber AS SYSNAME
DECLARE @subscriber_db AS SYSNAME
DECLARE @subscriberLogin AS SYSNAME
DECLARE @subscriberPassword AS SYSNAME
DECLARE @publicationName AS SYSNAME
 
SET @subscriber = N'sp.replica.dbserver'
SET @subscriber_db = N'SP.GraphQL.DB'
SET @subscriberLogin = N'sa'
SET @subscriberPassword = STRING_ESCAPE(N'S3cur3P@ssW0rd!', 'json')
SET @publicationName = N'CustomerTransactionalRepl'
 
EXEC sp_addsubscription @publication = @publicationName
    ,@subscriber = @subscriber
    ,@destination_db = @subscriber_db
    ,@subscription_type = N'Push'
    ,@sync_type = N'automatic'
    ,@article = N'all'
    ,@update_mode = N'read only'
    ,@subscriber_type = 0;
 
EXEC sp_addpushsubscription_agent @publication = @publicationName
    ,@subscriber = @subscriber
    ,@subscriber_db = @subscriber_db
    ,@subscriber_security_mode = 0
    ,@subscriber_login = @subscriberLogin
    ,@subscriber_password = @subscriberPassword
    ,@frequency_type = 64
    ,@frequency_interval = 0
    ,@frequency_relative_interval = 0
    ,@frequency_recurrence_factor = 0
    ,@frequency_subday = 0
    ,@frequency_subday_interval = 0
    ,@active_start_time_of_day = 0
    ,@active_end_time_of_day = 0
    ,@active_start_date = 0
    ,@active_end_date = 19950101;
GO