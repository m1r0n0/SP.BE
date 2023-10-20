--Create publication
USE [SP.Customer.DB];
GO
 
DECLARE @replicationdb AS SYSNAME;
DECLARE @publisherlogin AS SYSNAME;
DECLARE @publisherpassword AS SYSNAME;
DECLARE @publicationName AS SYSNAME
 
SET @replicationdb = N'SP.Customer.DB';
SET @publisherlogin = N'sa';
SET @publisherpassword = STRING_ESCAPE(N'S3cur3P@ssW0rd!', 'json');
SET @publicationName = N'CustomerTransactionalRepl'
 
EXEC sp_replicationdboption @dbname = @replicationdb
    ,@optname = N'publish'
    ,@value = N'true';
 
EXEC sp_addpublication @publication = @publicationName
    ,@description = N'Transactional publication of database ''SP.Customer.DB'' from Publisher ''''.'
    ,@retention = 0
    ,@allow_push = N'true'
    ,@repl_freq = N'continuous'
    ,@status = N'active'
    ,@independent_agent = N'true';
 
EXEC sp_addpublication_snapshot @publication = @publicationName
    ,@frequency_type = 1
    ,@frequency_interval = 1
    ,@frequency_relative_interval = 1
    ,@frequency_recurrence_factor = 0
    ,@frequency_subday = 8
    ,@frequency_subday_interval = 1
    ,@active_start_time_of_day = 0
    ,@active_end_time_of_day = 235959
    ,@active_start_date = 0
    ,@active_end_date = 0
    ,@publisher_security_mode = 0
    ,@publisher_login = @publisherlogin
    ,@publisher_password = @publisherpassword;
GO 

--Create Article
USE [SP.Customer.DB];
GO
 
EXEC sp_addarticle @publication = N'CustomerTransactionalRepl'
    ,@article = N'Customers'
    ,@source_owner = N'dbo'
    ,@source_object = N'Customers'
    ,@type = N'logbased'
    ,@description = NULL
    ,@creation_script = NULL
    ,@pre_creation_cmd = N'drop'
    ,@schema_option = 0x000000000803509D
    ,@identityrangemanagementoption = N'manual'
    ,@destination_table = N'Customers'
    ,@destination_owner = N'dbo'
    ,@vertical_partition = N'false';
GO

--Create Subscription
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


--Run Initial Snapshot creation job
USE [msdb]; 
GO
 
DECLARE @job1 SYSNAME;
 
SELECT @job1 = name FROM msdb.dbo.sysjobs
WHERE name LIKE '%-SP.Customer.DB-CustomerTransactionalRepl-%'
 
EXEC dbo.sp_start_job @job1
GO