FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /project

COPY ConsumerBLL ConsumerBLL
COPY ConsumerDLL ConsumerDLL
COPY MessageConsumer MessageConsumer

WORKDIR /project/MessageConsumer
RUN dotnet restore MessageConsumer.csproj && \
    dotnet build MessageConsumer.csproj -c Debug -o /app/debug

EXPOSE 7000
EXPOSE 7001
ENV ASPNETCORE_URLS=https://+:7001;http://+:7000

RUN dotnet dev-certs https --clean && dotnet dev-certs https --verbose

WORKDIR /app/debug
ENTRYPOINT dotnet MessageConsumer.dll