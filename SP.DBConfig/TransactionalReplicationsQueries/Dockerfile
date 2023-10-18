FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY distributor_init.sh /usr/scripts/
COPY subscriber_init.sh /usr/scripts/
ENTRYPOINT []