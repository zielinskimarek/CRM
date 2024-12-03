FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CRM.Deals.Api/CRM.Deals.Api.csproj", "CRM.Deals.Api/"]
RUN dotnet restore "CRM.Deals.Api/CRM.Deals.Api.csproj"
COPY . .
WORKDIR "/src/CRM.Deals.Api"
RUN dotnet build "CRM.Deals.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CRM.Deals.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRM.Deals.Api.dll"]
