# Build with NET6.0 SDK
# Platform: Win-x86

try {
    Push-Location
    cd ../../AutoClock/
    dotnet publish AutoClock.csproj -c Release --no-self-contained --framework net6.0 --runtime win-x86 --output bin/Release/net6.0/publish/win-x86
}
finally {
    Pop-Location
}