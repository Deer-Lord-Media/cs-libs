#/bin/bash

set -euo pipefail

NAME="${1}"
DESC="${2}"
LIBDIR="Libs/${NAME}"
LIBPROJ="${LIBDIR}/${NAME}.csproj"
TESTFILE="Tests/${NAME}Test.cs"

pre-flight() {
  if [ -e "$LIBDIR"]
  then
    return 1
  fi
  if [ -e "$LIBPROJ" ]
  then
    return 2
  fi
  if [ -e "$TESTFILE" ]
  then
    return 3
  fi
}

create-library() {
  dotnet new classlib --framework "netstandard2.1" -o "$LIBDIR"
}

insert-nuget-xml() {
  # clear end tags
  sed -e '/<\/PropertyGroup>/ d' -e '/<\/Project>/ d' -i "$LIBPROJ"

  cat >> "$LIBPROJ" <<EOF
    <PackageId>${NAME}</PackageId>
    <Version>0.0.1</Version>
    <Authors>William DaFriend</Authors>
    <Company>Deer Lord Media</Company>
    <PackageDescription>${DESC}</PackageDescription>
    <RepositoryUrl>https://github.com/Deer-Lord-Media/cs-libs</RepositoryUrl>
  </PropertyGroup>

</Project>
EOF
}

create-tests() {
  cat > "$TESTFILE" <<EOF

namespace Tests;
public class ${NAME}Test
{
    [Fact]
    public void Test${NAME}()
    {

    }
}
EOF
}

add-to-solution() {
  testproj="Tests/Tests.csproj"
  dotnet sln add "$LIBPROJ"
  dotnet sln add "${testproj}"
  dotnet add "${testproj}" reference "$LIBPROJ"
}

main() {
  pre-flight
  create-library
  insert-nuget-xml
  create-tests
  add-to-solution
}

main
