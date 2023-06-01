cd 512Labs
mkdir -p $NAME
cd $NAME
dotnet new classlib -o Library
dotnet new xunit -o Tests
cd ../
dotnet sln add $NAME/Library/Library.csproj
dotnet sln add $NAME/Tests/Tests.csproj
dotnet add $NAME/Tests/Tests.csproj reference $NAME/Library/Library.csproj
