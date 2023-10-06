FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SP.Customer/SP.Customer.API/SP.Customer.API.csproj", "SP.Customer/SP.Customer.API/"]
COPY ["SP.Customer/SP.Customer.BusinessLayer/SP.Customer.BusinessLayer.csproj", "SP.Customer/SP.Customer.BusinessLayer/"]
COPY ["SP.Customer/SP.Customer.DataAccessLayer/SP.Customer.DataAccessLayer.csproj", "SP.Customer/SP.Customer.DataAccessLayer/"]
RUN dotnet restore "SP.Customer/SP.Customer.API/SP.Customer.API.csproj"
COPY . .
WORKDIR "/src/SP.Customer/SP.Customer.API"
RUN dotnet build "SP.Customer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SP.Customer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SP.Customer.API.dll"]