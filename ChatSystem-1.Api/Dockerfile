FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5199

ENV ASPNETCORE_URLS=http://*:$PORT

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["ChatSystem-1.Api/ChatSystem-1.Api.csproj", "ChatSystem-1.Api/"]
RUN dotnet restore "ChatSystem-1.Api/ChatSystem-1.Api.csproj"
COPY . .
WORKDIR "/src/ChatSystem-1.Api"
RUN dotnet build "ChatSystem-1.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ChatSystem-1.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatSystem-1.Api.dll"]
