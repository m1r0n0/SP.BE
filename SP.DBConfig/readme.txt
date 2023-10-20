0. Create ReplData directory in distributor container by exec. next command: docker exec -it SP.DBServer mkdir /var/opt/mssql/data/ReplData/

1. Create all distibutor dbs' tables (done in migrations on startup)
2. Create SP.GraphQL.DB db at publisher server
3. Configure Distributor
4. Configure Publisher
5. Configure Replications