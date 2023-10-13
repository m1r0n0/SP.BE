USE [SP.Provider.DB];
GO
 
EXEC sp_addarticle @publication = N'ProviderTransactionalRepl'
    ,@article = N'Providers'
    ,@source_owner = N'dbo'
    ,@source_object = N'Providers'
    ,@type = N'logbased'
    ,@description = NULL
    ,@creation_script = NULL
    ,@pre_creation_cmd = N'drop'
    ,@schema_option = 0x000000000803509D
    ,@identityrangemanagementoption = N'manual'
    ,@destination_table = N'Providers'
    ,@destination_owner = N'dbo'
    ,@vertical_partition = N'false';
GO