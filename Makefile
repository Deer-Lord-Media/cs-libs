
clean:
	dotnet nuget locals all --clear
	dotnet clean

restore:
	dotnet restore

build: restore
	dotnet build $(MODULE)

test: build
	dotnet test --no-build --verbosity normal
