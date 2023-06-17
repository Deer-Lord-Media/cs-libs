#/bin/bash

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
  name="${1}"
  dotnet new classlib -o "${name}"
}

insert-nuget-xml() {
  name="${1}"
  description="${2}"
  csproj="${name}/${name}.csproj"

  # clear end tags
  sed -e '/<\/PropertyGroup>/ d' -e '/<\/Project>/ d' -i "$csproj"

  cat >> "$csproj" <<EOF
    <PackageId>${name}</PackageId>
    <Version>0.0.1</Version>
    <Authors>William Da Friend</Authors>
    <Company>Deer Lord Entertainment</Company>
    <PackageDescription>${description}</PackageDescription>
    <RepositoryUrl>https://github.com/Deer-Lord-Entertainment/cs-libs</RepositoryUrl>
  </PropertyGroup>

</Project>
EOF
}

main() {
  if [ -e "${NAME}"]
  then
    return 1
  fi
  create-library "${NAME}" "${DESC}"
  insert-nuget-xml "${NAME}" 
}

main
