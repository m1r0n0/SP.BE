FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SP.GraphQL/SP.GraphQL.API/SP.GraphQL.API.csproj", "SP.GraphQL/SP.GraphQL.API/"]
COPY ["SP.GraphQL/SP.GraphQL.BusinessLayer/SP.GraphQL.BusinessLayer/SP.GraphQL.BusinessLayer.csproj", "SP.GraphQL/SP.GraphQL.BusinessLayer/SP.GraphQL.BusinessLayer/"]
COPY ["SP.GraphQL/SP.GraphQL.DataAccessLayer/SP.GraphQL.DataAccessLayer.csproj", "SP.GraphQL/SP.GraphQL.DataAccessLayer/"]
RUN dotnet restore "SP.GraphQL/SP.GraphQL.API/SP.GraphQL.API.csproj"
COPY . .
WORKDIR "/src/SP.GraphQL/SP.GraphQL.API"
RUN dotnet build "SP.GraphQL.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SP.GraphQL.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SP.GraphQL.API.dll"]