
clean:
	dotnet nuget locals all --clear
	dotnet clean

restore:
	dotnet restore

build-docker:
	docker build --no-cache -t cs-libs .

test-docker: build-docker
	docker run --env-file .env -it cs-libs dotnet test

build-all: restore
	dotnet build

build-module:
	dotnet build $(MODULE)

test-all: build
	dotnet test --no-build --verbosity normal

test-module:
	dotnet build $(MODULE)
	dotnet test --no-build --verbosity normal --filter "FullyQualifiedName~$(MODULE)Test"
