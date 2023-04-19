# Build with NET6.0 SDK
# Platform: Win-arm64

try {
    Push-Location
    cd ../../AutoClock/
    dotnet publish AutoClock.csproj -c Release --no-self-contained --framework net6.0 --runtime win-arm64 --output bin/Release/net6.0/publish/win-arm64
}
finally {
    Pop-Location
}