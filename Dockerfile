FROM mcr.microsoft.com/dotnet/sdk:8.0

ARG MODULE

WORKDIR /opt
COPY ./src /opt
RUN dotnet build
