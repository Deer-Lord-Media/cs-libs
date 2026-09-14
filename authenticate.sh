#!/bin/bash

# add source
dotnet nuget add source https://nuget.pkg.github.com/Deer-Lord-Media/index.json \
  --name github \
  --username YOUR_GH_USERNAME \
  --password $(gh auth token) \
  --store-password-in-clear-text

# update existing source, such as when token changes
dotnet nuget update source github \
    --username $USERNAME \
    --password $TOKEN \
