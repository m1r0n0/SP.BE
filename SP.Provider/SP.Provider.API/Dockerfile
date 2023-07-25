FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["SP.Provider/SP.Provider.API/SP.Provider.API.csproj", "SP.Provider/SP.Provider.API/"]
COPY ["SP.Provider/SP.Provider.BusinessLayer/SP.Provider.BusinessLayer.csproj", "SP.Provider/SP.Provider.BusinessLayer/"]
COPY ["SP.Provider/SP.Provider.DataAccessLayer/SP.Provider.DataAccessLayer.csproj", "SP.Provider/SP.Provider.DataAccessLayer/"]
RUN dotnet restore "SP.Provider/SP.Provider.API/SP.Provider.API.csproj"
COPY . .
WORKDIR "/src/SP.Provider/SP.Provider.API"
RUN dotnet build "SP.Provider.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SP.Provider.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SP.Provider.API.dll"]