#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 1433 

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HarborManagementAPI.csproj", "."]
RUN dotnet restore "./HarborManagementAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HarborManagementAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HarborManagementAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

EXPOSE 5100/tcp
ENV ASPNETCORE_URLS http://*:5100
HEALTHCHECK --interval=30s --timeout=3s --retries=1 CMD curl --silent --fail http://localhost:5100/hc || exit 1

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HarborManagementAPI.dll"]