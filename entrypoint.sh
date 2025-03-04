#!/bin/bash

set -euo pipefail

dotnet nuget add source \
    --username $GITHUB_USERNAME \
    --password $GITHUB_TOKEN \
    --store-password-in-clear-text \
    --name github \
    "https://nuget.pkg.github.com/Deer-Lord-Media/index.json"

exec "$@"
