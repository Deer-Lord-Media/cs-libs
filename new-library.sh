#/bin/bash

NAME="${1}"

# mkdir -p "$NAME"
# cd "$NAME"
# dotnet new sln --name "$NAME"
# dotnet new classlib -o Library
# dotnet new xunit -o Tests
# dotnet sln add Library/Library.csproj
# dotnet sln add Tests/Tests.csproj
# dotnet add Tests/Tests.csproj reference Library/Library.csproj


create-library() {
    # just a basic csproj lib
    dotnet new classlib -o "${NAME}"
}

insert-nuget-xml() {
    csproj="${NAME}/${NAME}.csproj"

    # clear end tags
    sed -e '/<\/PropertyGroup>/ d' -e '/<\/Project>/ d' -i "$csproj"

    cat >> "$csproj" <<EOF
    <PackageId>${NAME}</PackageId>
    <Version>1.0.0</Version>
    <Authors>William Da Friend</Authors>
    <Company>Deer Lord Entertainment</Company>
    <PackageDescription>Buff Tracker</PackageDescription>
    <RepositoryUrl>https://github.com/Deer-Lord-Entertainment/${NAME}</RepositoryUrl>
  </PropertyGroup>

</Project>
EOF
}
