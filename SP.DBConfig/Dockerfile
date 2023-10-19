# Use the base image you want to copy
FROM mcr.microsoft.com/mssql/server:2022-latest AS base

# Create app directory
WORKDIR /usr/src/app

# Copy initialization scripts
COPY . /usr/src/app

# Expose port 1433 in case accessing from other container
# Expose port externally from docker-compose.yml
EXPOSE 1433

# Run Microsoft SQL Server and initialization script (at the same time)
# CMD /bin/bash ./entrypoint.sh


# WORKDIR /
# COPY distributor_init.sh /usr/scripts/
# COPY subscriber_init.sh /usr/scripts/
# ENTRYPOINT []