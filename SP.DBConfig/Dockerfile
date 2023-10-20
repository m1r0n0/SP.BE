# Use the base image you want to copy
FROM mcr.microsoft.com/mssql/server:2022-latest AS base

# Create app directory
WORKDIR /usr/src/app

# Copy initialization scripts
COPY . /usr/src/app

WORKDIR /var/opt/mssql/data/
RUN mkdir -p ReplData