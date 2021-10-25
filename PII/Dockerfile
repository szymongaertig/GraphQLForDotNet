FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PII/PII.csproj", "PII/"]
RUN dotnet restore "PII/PII.csproj"
COPY . .
WORKDIR "/src/PII"
RUN dotnet build "PII.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PII.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PII.dll"]
