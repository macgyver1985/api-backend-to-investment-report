FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /api-investment-report
COPY ./src ./src
RUN dotnet build ./src --configuration Release
RUN dotnet publish ./src/InvestmentReport.WebApi/InvestmentReport.WebApi.csproj -c Release -o ./publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV ASPNETCORE_ENVIRONMENT=Production
ENV REDIS_URL=$REDIS_URL
WORKDIR /wwwroot
RUN mkdir logs
COPY --from=build /api-investment-report/publish .

RUN useradd -m macgyver1985
USER macgyver1985

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet ./InvestmentReport.WebApi.dll