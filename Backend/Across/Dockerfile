#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Across/Across.csproj", "Across/"]
COPY ["UseCases/UseCases.csproj", "UseCases/"]
COPY ["Infrastructure.Interfaces/Infrastructure.Interfaces.csproj", "Infrastructure.Interfaces/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["DataAccess.Interfaces/DataAccess.Interfaces.csproj", "DataAccess.Interfaces/"]
COPY ["DataAccess.BaseImplementation/DataAccess.BaseImplementation.csproj", "DataAccess.BaseImplementation/"]
COPY ["DataAccess.SqlLite/DataAccess.SqlLite.csproj", "DataAccess.SqlLite/"]
COPY ["DataAccess.MySql/DataAccess.MySql.csproj", "DataAccess.MySql/"]
COPY ["ApplicationServices.Implementation/ApplicationServices.Implementation.csproj", "ApplicationServices.Implementation/"]
COPY ["ApplicationServices/ApplicationServices.Interfaces.csproj", "ApplicationServices/"]
COPY ["BackgroundJobs.Implementation/BackgroundJobs.Implementation.csproj", "BackgroundJobs.Implementation/"]
COPY ["BackgroundJobs.Interfaces/BackgroundJobs.Interfaces.csproj", "BackgroundJobs.Interfaces/"]
COPY ["Storage.Implementation/Storage.Implementation.csproj", "Storage.Implementation/"]
COPY ["Storage.Interfaces/Storage.Interfaces.csproj", "Storage.Interfaces/"]

RUN dotnet restore "Across/Across.csproj"
COPY . .
WORKDIR "/src/Across"
RUN dotnet build "Across.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Across.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Across.dll"]