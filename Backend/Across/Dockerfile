#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BackendWashMe/BackendWashMe.csproj", "BackendWashMe/"]
COPY ["UseCases/UseCases.csproj", "UseCases/"]
COPY ["Infrastructure.Interfaces/Infrastructure.Interfaces.csproj", "Infrastructure.Interfaces/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["DataAccess.Interfaces/DataAccess.Interfaces.csproj", "DataAccess.Interfaces/"]
COPY ["DataAccess.BaseImplementation/DataAccess.BaseImplementation.csproj", "DataAccess.BaseImplementation/"]
COPY ["DataAccess.SqlLite/DataAccess.SqlLite.csproj", "DataAccess.SqlLite/"]
COPY ["DataAccess.MySql/DataAccess.MySql.csproj", "DataAccess.MySql/"]
COPY ["ApplicationServices.Implementation/ApplicationServices.Implementation.csproj", "ApplicationServices.Implementation/"]
COPY ["ApplicationServices/ApplicationServices.Interfaces.csproj", "ApplicationServices/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "BackendWashMe/BackendWashMe.csproj"
COPY . .
WORKDIR "/src/BackendWashMe"
RUN dotnet build "BackendWashMe.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackendWashMe.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendWashMe.dll"]