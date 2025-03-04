CURRENT_DIR := $(shell pwd)
IMAGE_NAME := cs-libs
RUN_CONTAINER := docker run -e GITHUB_USERNAME=$(GITHUB_USERNAME) -e GITHUB_TOKEN=$(GITHUB_TOKEN) --mount type=bind,source=$(CURRENT_DIR)/bin,target=/tmp -t $(IMAGE_NAME)

build:
	docker build --build-arg MODULE=$(MODULE) -t $(IMAGE_NAME) -f Dockerfile src

test: build
	$(RUN_CONTAINER) dotnet test --no-build --verbosity normal --filter "FullyQualifiedName~$(MODULE)Test"

package: test
	$(RUN_CONTAINER) dotnet pack Libs/$(MODULE) --configuration Release -o /tmp/output

push:
	$(RUN_CONTAINER) dotnet nuget push --skip-duplicate "/tmp/output/$(MODULE).$(VERSION).nupkg" --source "https://nuget.pkg.github.com/Deer-Lord-Media/index.json" --api-key $(GITHUB_TOKEN)
