FROM node:18 AS node_build

WORKDIR /app

COPY src/homebudget-frontend ./

RUN yarn install
RUN yarn build

FROM mcr.microsoft.com/dotnet/sdk:9.0 as dotnet_build

WORKDIR /app

COPY src/HomeBudget.API ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app

COPY --from=node_build /app/dist ./wwwroot
COPY --from=dotnet_build /app/out ./

ENTRYPOINT ["dotnet", "HomeBudget.API.dll"]