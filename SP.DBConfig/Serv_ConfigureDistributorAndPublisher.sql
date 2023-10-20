--Configure Distri
USE [master];
GO
  
DECLARE @distributor AS SYSNAME;
DECLARE @distributorlogin AS SYSNAME;
DECLARE @distributorpassword AS SYSNAME;
DECLARE @Server SYSNAME;
  
SELECT @Server = @@servername;
  
SET @distributor = @Server;
SET @distributorlogin = N'sa';
SET @distributorpassword = STRING_ESCAPE(N'S3cur3P@ssW0rd!', 'json');
  
EXEC sp_adddistributor @distributor = @distributor;
  
EXEC sp_adddistributiondb @database = N'distribution'
    ,@log_file_size = 2
    ,@deletebatchsize_xact = 5000
    ,@deletebatchsize_cmd = 2000
    ,@security_mode = 0
    ,@login = @distributorlogin
    ,@password = @distributorpassword;
GO
 
USE [distribution];
GO
  
DECLARE @snapshotdirectory AS NVARCHAR(500);
  
SET @snapshotdirectory = N'/var/opt/mssql/data/ReplData/';
  
IF (NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'UIProperties' AND type = 'U '))
    CREATE TABLE UIProperties (id INT);
  
IF (EXISTS (SELECT * FROM::fn_listextendedproperty('SnapshotFolder', 'user', 'dbo', 'table', 'UIProperties', NULL, NULL)))
    EXEC sp_updateextendedproperty N'SnapshotFolder'
        ,@snapshotdirectory
        ,'user'
        ,dbo
        ,'table'
        ,'UIProperties'
ELSE
    EXEC sp_addextendedproperty N'SnapshotFolder'
        ,@snapshotdirectory
        ,'user'
        ,dbo
        ,'table'
        ,'UIProperties';
GO

--Configure Publisher
USE [distribution];
GO
 
DECLARE @publisher AS SYSNAME;
DECLARE @distributorlogin AS SYSNAME;
DECLARE @distributorpassword AS SYSNAME;
DECLARE @Server SYSNAME;
 
SELECT @Server = @@servername;
 
SET @publisher = @Server;
SET @distributorlogin = N'sa';
SET @distributorpassword = STRING_ESCAPE(N'S3cur3P@ssW0rd!', 'json');
 
EXEC sp_adddistpublisher @publisher = @publisher
    ,@distribution_db = N'distribution'
    ,@security_mode = 0
    ,@login = @distributorlogin
    ,@password = @distributorpassword
    ,@working_directory = N'/var/opt/mssql/data/ReplData'
    ,@trusted = N'false'
    ,@thirdparty_flag = 0
    ,@publisher_type = N'MSSQLSERVER';
GO