#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/api/dart.screening.api/dart.screening.api.csproj", "src/api/dart.screening.api/"]
RUN dotnet restore "src/api/dart.screening.api/dart.screening.api.csproj"
COPY . .
WORKDIR "/src/src/api/dart.screening.api"
RUN dotnet build "dart.screening.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dart.screening.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dart.screening.api.dll"]