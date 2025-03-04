FROM mcr.microsoft.com/dotnet/sdk:8.0

ARG MODULE

WORKDIR /opt
COPY Libs/${MODULE} /opt/Libs/${MODULE}
COPY Tests /opt/Tests
COPY cs-libs.sln /opt/
COPY entrypoint.sh /usr/bin/entrypoint.sh
RUN dotnet build

ENTRYPOINT ["bash", "/usr/bin/entrypoint.sh"]
