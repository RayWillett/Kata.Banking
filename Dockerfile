FROM mcr.microsoft.com/dotnet/aspnet:7.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY . /src
WORKDIR /src
RUN dotnet build "Kata.Banking.Web/Kata.Banking.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kata.Banking.Web/Kata.Banking.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .  