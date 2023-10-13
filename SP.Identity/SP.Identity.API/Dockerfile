FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["SP.Identity/SP.Identity.API/SP.Identity.API.csproj", "SP.Identity/SP.Identity.API/"]
COPY ["SP.Identity/SP.Identity.BusinessLayer/SP.Identity.BusinessLayer.csproj", "SP.Identity/SP.Identity.BusinessLayer/"]
COPY ["SP.Identity/SP.Identity.DataAccessLayer/SP.Identity.DataAccessLayer.csproj", "SP.Identity/SP.Identity.DataAccessLayer/"]
RUN dotnet restore "SP.Identity/SP.Identity.API/SP.Identity.API.csproj"
COPY . .
WORKDIR "/src/SP.Identity/SP.Identity.API"
RUN dotnet build "SP.Identity.API.csproj" -c Release -o /app/build

#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS migration
#WORKDIR /src
#COPY . .
#RUN dotnet restore "SP.Identity/SP.Identity.DataAccessLayer/SP.Identity.DataAccessLayer.csproj"
#COPY . .
#WORKDIR "/src/SP.Identity/SP.Identity.DataAccessLayer"
#RUN dotnet build "SP.Identity.DataAccessLayer.csproj" -c Release -o /app/migration

FROM build AS publish
RUN dotnet publish "SP.Identity.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SP.Identity.API.dll"]

FROM mcr.microsoft.com/dotnet/sdk:7.0
COPY init.sh /usr/local/bin/
ENTRYPOINT []