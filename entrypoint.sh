#!/bin/bash

dotnet nuget add source \
    --username $GITHUB_USER \
    --password $GITHUB_TOKEN \
    --store-password-in-clear-text \
    --name github \
    "https://nuget.pkg.github.com/Deer-Lord-Media/index.json"

exec "$@"
