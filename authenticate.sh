#!/bin/bash

# add source
dotnet nuget add source \
    --username $USERNAME \
    --password $TOKEN \
    --store-password-in-clear-text \
    --name github \
    "https://nuget.pkg.github.com/Deer-Lord-Media/index.json"

# update existing source, such as when token changes
dotnet nuget update source github \
    --username $USERNAME \
    --password $TOKEN \
