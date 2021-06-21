FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /api-investiment-report
COPY ./src ./src
RUN dotnet build ./src --configuration Release
RUN dotnet publish ./src/InvestimentReport.WebApi/InvestimentReport.WebApi.csproj -c Release -o ./publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV ASPNETCORE_ENVIRONMENT=Production
ENV REDIS_URL=$REDIS_URL
WORKDIR /wwwroot
COPY --from=build /api-investiment-report/publish .

RUN useradd -m macgyver1985
USER macgyver1985

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet InvestimentReport.WebApi.dll