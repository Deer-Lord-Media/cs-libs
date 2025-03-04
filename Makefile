# Convert MODULE to lowercase
IMAGE_NAME := cs-libs-$(shell echo $(MODULE) | tr '[:upper:]' '[:lower:]')
RUN_CONTAINER := docker run -e GITHUB_USERNAME=$(GITHUB_USERNAME) -e GITHUB_TOKEN=$(GITHUB_TOKEN) -t $(IMAGE_NAME)

build:
	docker build --build-arg MODULE=$(MODULE) -t $(IMAGE_NAME) .

test: build
	$(RUN_CONTAINER) dotnet test --no-build --verbosity normal --filter "FullyQualifiedName~$(MODULE)Test"

package: test
	$(RUN_CONTAINER) dotnet pack Libs/$(MODULE) --configuration Release

push: test
	$(RUN_CONTAINER) dotnet nuget push --skip-duplicate "Libs/$(MODULE)/bin/Release/$(MODULE).$(VERSION).nupkg" --source "https://nuget.pkg.github.com/Deer-Lord-Media/index.json" --api-key $(GITHUB_TOKEN)
