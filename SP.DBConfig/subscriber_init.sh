 #! /bin/sh
 sleep 45s

 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P S3cur3P@ssW0rd! -d master -i /usr/src/app/'Serv_Replica_CreateGraphQLDB.sql'
