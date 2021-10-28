#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install --global dotnet-ef
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TheTruth.Api/TheTruth.Api.csproj", "TheTruth.Api/"]
RUN dotnet restore "TheTruth.Api/TheTruth.Api.csproj"
COPY . .
WORKDIR "/src/TheTruth.Api"
RUN dotnet build "TheTruth.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheTruth.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheTruth.Api.dll"]