 #! /bin/sh
 # Wait to be sure that SQL Server came up
 sleep 90s


 /opt/mssql-tools/bin/sqlcmd -S localhost,1551 -U sa -P 'S3cur3P@ssW0rd!' -d master -i '2-CreateGraphQLDB.sql'