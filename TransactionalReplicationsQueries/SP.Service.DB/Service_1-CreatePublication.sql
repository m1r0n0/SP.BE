USE [SP.Service.DB];
GO
 
DECLARE @replicationdb AS SYSNAME;
DECLARE @publisherlogin AS SYSNAME;
DECLARE @publisherpassword AS SYSNAME;
DECLARE @publicationName AS SYSNAME
 
SET @replicationdb = N'SP.Service.DB';
SET @publisherlogin = N'sa';
SET @publisherpassword = STRING_ESCAPE(N'S3cur3P@ssW0rd!', 'json');
SET @publicationName = N'ServiceTransactionalRepl'
 
EXEC sp_replicationdboption @dbname = @replicationdb
    ,@optname = N'publish'
    ,@value = N'true';
 
EXEC sp_addpublication @publication = @publicationName
    ,@description = N'Transactional publication of database ''SP.Service.DB'' from Publisher ''''.'
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