FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["./code/API/API.csproj", "API/"]
COPY ["./code/Application/Application.csproj", "Application/"]
COPY ["./code/Domain/Domain.csproj", "Domain/"]
COPY ["./code/Storage/Storage.csproj", "Storage/"]
RUN dotnet restore "API/API.csproj"

COPY ./code .
WORKDIR "/src/API"
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "API.dll"]