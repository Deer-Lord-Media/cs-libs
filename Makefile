
build:
	dotnet restore
	dotnet build

test: build
	dotnet restore
	dotnet test
