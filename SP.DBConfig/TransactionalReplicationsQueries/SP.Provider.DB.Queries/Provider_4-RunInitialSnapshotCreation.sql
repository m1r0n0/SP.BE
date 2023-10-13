USE [msdb]; 
GO
 
DECLARE @job1 SYSNAME;
 
SELECT @job1 = name FROM msdb.dbo.sysjobs
WHERE name LIKE '%-SP.Provider.DB-ProviderTransactionalRepl-%'
 
EXEC dbo.sp_start_job @job1
GO