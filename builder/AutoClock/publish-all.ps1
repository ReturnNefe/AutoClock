# Build for all platforms

Write-Host "Publishing Linux-arm64" -ForegroundColor DarkYellow
./publish-linux-arm64.ps1

Write-Host "Publishing Linux-x64" -ForegroundColor DarkYellow
./publish-linux-x64.ps1

Write-Host "Publishing OSX-x64" -ForegroundColor DarkYellow
./publish-osx-x64.ps1

Write-Host "Publishing Windows-arm64" -ForegroundColor DarkYellow
./publish-win-arm64.ps1

Write-Host "Publishing Windows-x64" -ForegroundColor DarkYellow
./publish-win-x64.ps1

Write-Host "Publishing Windows-x86" -ForegroundColor DarkYellow
./publish-win-x86.ps1