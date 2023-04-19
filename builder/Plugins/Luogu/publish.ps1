# Build with NET6.0 SDK
# Platform: All

try {
    Push-Location
    cd ../../../Unit/Plugins/Luogu
    dotnet publish Luogu.csproj -c Release --no-self-contained --framework net6.0 --output bin/Release/net6.0/publish/
}
finally {
    Pop-Location
}