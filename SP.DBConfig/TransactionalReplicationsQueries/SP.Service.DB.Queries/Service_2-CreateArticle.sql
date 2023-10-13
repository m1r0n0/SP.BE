USE [SP.Service.DB];
GO
 
EXEC sp_addarticle @publication = N'ServiceTransactionalRepl'
    ,@article = N'Services'
    ,@source_owner = N'dbo'
    ,@source_object = N'Services'
    ,@type = N'logbased'
    ,@description = NULL
    ,@creation_script = NULL
    ,@pre_creation_cmd = N'drop'
    ,@schema_option = 0x000000000803509D
    ,@identityrangemanagementoption = N'manual'
    ,@destination_table = N'Services'
    ,@destination_owner = N'dbo'
    ,@vertical_partition = N'false';
GO

USE [SP.Service.DB];
GO
 
EXEC sp_addarticle @publication = N'ServiceTransactionalRepl'
    ,@article = N'Events'
    ,@source_owner = N'dbo'
    ,@source_object = N'Events'
    ,@type = N'logbased'
    ,@description = NULL
    ,@creation_script = NULL
    ,@pre_creation_cmd = N'drop'
    ,@schema_option = 0x000000000803509D
    ,@identityrangemanagementoption = N'manual'
    ,@destination_table = N'Events'
    ,@destination_owner = N'dbo'
    ,@vertical_partition = N'false';
GO