#! /bin/sh
# Wait to be sure that SQL Server came up


sleep 60s

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P S3cur3P@ssW0rd! -d master -i /usr/src/app/'Serv_ConfigureDistributorAndPublisher.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P S3cur3P@ssW0rd! -d master -i /usr/src/app/'Serv_Customer_CreateTransactionalReplication.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P S3cur3P@ssW0rd! -d master -i /usr/src/app/'Serv_Provider_CreateTransactionalReplication.sql'
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P S3cur3P@ssW0rd! -d master -i /usr/src/app/'Serv_Service_CreateTransactionalReplication.sql'