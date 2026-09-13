FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0

ENV DEBIAN_FRONTEND=noninteractive

RUN rm -f /etc/apt/sources.list.d/yarn.list \
    && apt-get update \
    && apt-get install -y --no-install-recommends python3-venv \
    && rm -rf /var/lib/apt/lists/*

RUN apt update -y && apt install -y libx11-6 \
    libxi6 \
    libxrender1 \
    libxfixes3 \
    libsm6 \
    libgl1 \
    libglib2.0-0 \
    libxkbcommon0 \
    && rm -rf /var/lib/apt/lists/*
