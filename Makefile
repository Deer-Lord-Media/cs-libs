# Convert MODULE to lowercase
IMAGE_NAME := cs-libs-$(shell echo $(MODULE) | tr '[:upper:]' '[:lower:]')

build:
	docker build --no-cache --build-arg MODULE=$(MODULE) -t $(IMAGE_NAME) .

test: build
	docker run --env-file .env -it $(IMAGE_NAME) dotnet test --no-build --verbosity normal --filter "FullyQualifiedName~$(MODULE)Test"

package: test
	docker run --env-file .env -it $(IMAGE_NAME) dotnet pack Libs/$(MODULE) --configuration Release

push: test
	docker run --env-file .env -it $(IMAGE_NAME) dotnet nuget push --skip-duplicate "Libs/$(MODULE)/bin/Release/$(MODULE).$(VERSION).nupkg" --source "https://nuget.pkg.github.com/Deer-Lord-Media/index.json" --api-key $(TOKEN)
