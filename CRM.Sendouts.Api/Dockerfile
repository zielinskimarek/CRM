FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CRM.Sendouts.Api/CRM.Sendouts.Api.csproj", "CRM.Sendouts.Api/"]
RUN dotnet restore "CRM.Sendouts.Api/CRM.Sendouts.Api.csproj"
COPY . .
WORKDIR "/src/CRM.Sendouts.Api"
RUN dotnet build "CRM.Sendouts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CRM.Sendouts.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRM.Sendouts.Api.dll"]
