# Build with NET6.0 SDK
# Platform: Linux-x64

try {
    Push-Location
    cd ../../AutoClock/
    dotnet publish AutoClock.csproj -c Release --no-self-contained --framework net6.0 --runtime linux-x64 --output bin/Release/net6.0/publish/linux-x64
}
finally {
    Pop-Location
}