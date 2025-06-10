
build:
	dotnet build ./src

test: build
	dotnet test ./src/Tests --no-build --verbosity normal --filter "FullyQualifiedName~$(MODULE)Test"

package: test
	dotnet pack ./src/Libs/$(MODULE) --configuration Release -o /tmp/output

# TODO: update and ensure this works as expected
# push:
# 	dotnet nuget push --skip-duplicate "/tmp/output/$(MODULE).$(VERSION).nupkg" --source "https://nuget.pkg.github.com/Deer-Lord-Media/index.json" --api-key $(GITHUB_TOKEN)
