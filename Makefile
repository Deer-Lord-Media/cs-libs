build:
	docker build --no-cache -t cs-libs .

test: build
	docker run --env-file .env -it cs-libs dotnet test --no-build --verbosity normal

test-module: build
	docker run --env-file .env -it cs-libs dotnet test --no-build --verbosity normal --filter "FullyQualifiedName~$(MODULE)Test"

package:
	docker run --env-file .env -it cs-libs dotnet pack Libs/$(MODULE) --configuration Release

push:
	dotnet nuget push --skip-duplicate "Libs/$(MODULE)/bin/Release/$(MODULE).$(VERSION).nupkg" --source "https://nuget.pkg.github.com/Deer-Lord-Media/index.json" --api-key $(TOKEN)
