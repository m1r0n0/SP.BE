USE [msdb]; 
GO
 
DECLARE @job1 SYSNAME;
 
SELECT @job1 = name FROM msdb.dbo.sysjobs
WHERE name LIKE '%-SP.Customer.DB-CustomerTransactionalRepl-%'
 
EXEC dbo.sp_start_job @job1
GO