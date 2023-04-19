# Build with NET6.0 SDK
# Platform: osx-x64

try {
    Push-Location
    cd ../../AutoClock/
    dotnet publish AutoClock.csproj -c Release --no-self-contained --framework net6.0 --runtime osx-x64 --output bin/Release/net6.0/publish/osx-x64
}
finally {
    Pop-Location
}