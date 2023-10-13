FROM mcr.microsoft.com/mssql/server:2022-latest AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/mssql/server:2022-latest AS build
WORKDIR /src
COPY . .

#FROM build AS publish
#RUN dotnet publish "SP.GraphQL.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "SP.GraphQL.API.dll"]