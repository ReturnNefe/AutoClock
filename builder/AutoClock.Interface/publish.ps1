# Build with NET6.0 SDK
# Platform: All

try {
    Push-Location
    cd ../../AutoClock.Interface
    dotnet publish AutoClock.Interface.csproj -c Release --no-self-contained --framework net6.0 --output bin/Release/net6.0/publish/
}
finally {
    Pop-Location
}