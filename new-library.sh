#/bin/bash

set -euo pipefail

NAME="${1}"
DESC="${2}"

# mkdir -p "$name"
# cd "$name"
# dotnet new sln --name "$name"
# dotnet new classlib -o Library
# dotnet new xunit -o Tests
# dotnet sln add Library/Library.csproj
# dotnet sln add Tests/Tests.csproj
# dotnet add Tests/Tests.csproj reference Library/Library.csproj

create-library() {
  dotnet new classlib -o "${NAME}"
}

insert-nuget-xml() {
  csproj="${NAME}/${NAME}.csproj"

  # clear end tags
  sed -e '/<\/PropertyGroup>/ d' -e '/<\/Project>/ d' -i "$csproj"

  cat >> "$csproj" <<EOF
    <PackageId>${NAME}</PackageId>
    <Version>0.0.1</Version>
    <Authors>William DaFriend</Authors>
    <Company>Deer Lord Entertainment</Company>
    <PackageDescription>${DESC}</PackageDescription>
    <RepositoryUrl>https://github.com/Deer-Lord-Entertainment/cs-libs</RepositoryUrl>
  </PropertyGroup>

</Project>
EOF
}

create-tests() {
  testfile="Tests/${NAME}.cs"
  if [ -e "${testfile}" ]
  then
    return 1
  fi
  cat > "Tests/${NAME}.cs" <<EOF
namespace Tests;

public class ${NAME}Test
{
    [Fact]
    public void Test1()
    {

    }
}
EOF
}

add-to-solution() {
  libproj="${NAME}/${NAME}.csproj"
  testproj="Tests/Tests.csproj"
  dotnet sln add "${libproj}"
  dotnet sln add "${testproj}"
  dotnet add "${testproj}" reference "${libproj}"
}

main() {
  if [ ! -e "${NAME}"]
  then
    create-library
    insert-nuget-xml
    create-tests
    add-to-solution
  fi
}
