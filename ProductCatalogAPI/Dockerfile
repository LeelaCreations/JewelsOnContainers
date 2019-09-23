FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
EXPOSE 80
#copy everything, restore and publish
COPY . .
RUN dotnet restore
RUN dotnet publish "ProductCatalogAPI.csproj" -c Release -o /app
#Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ProductCatalogAPI.dll"]
