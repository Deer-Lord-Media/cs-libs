FROM mcr.microsoft.com/dotnet/sdk:8.0

ARG MODULE

WORKDIR /opt
COPY ./src/Libs /opt/Libs
COPY ./src/Tests /opt/Tests
COPY ./src/cs-libs.sln /opt/cs-libs.sln
RUN dotnet build
