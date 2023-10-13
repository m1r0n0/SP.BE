USE [distribution];
GO
 
DECLARE @publisher AS SYSNAME;
DECLARE @distributorlogin AS SYSNAME;
DECLARE @distributorpassword AS SYSNAME;
DECLARE @Server SYSNAME;
 
--SELECT @Server = @@servername;
SET @Server = N'SP.DBServer'
 
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