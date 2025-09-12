
install-deps:
	dotnet add ./src/Tests/Tests.csproj package coverlet.collector
	dotnet tool install -g dotnet-reportgenerator-globaltool

build:
	dotnet build ./src/Libs/$(MODULE)

build-all:
	dotnet build ./src

test: build
	dotnet test ./src/Tests --verbosity normal --filter "FullyQualifiedName~$(MODULE)Test"

test-all: build-all
	dotnet test ./src/Tests --collect:"XPlat Code Coverage"
	reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html

package: test
	dotnet pack ./src/Libs/$(MODULE) --configuration Release -o /tmp/output

push: package
	dotnet nuget push --skip-duplicate "/tmp/output/$(MODULE).$(VERSION).nupkg" --source "https://nuget.pkg.github.com/Deer-Lord-Media/index.json" --api-key $(GITHUB_TOKEN)
