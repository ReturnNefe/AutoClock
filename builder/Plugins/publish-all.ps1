# Build for all plugins

try {
    Push-Location
    Write-Host "Publishing EmailSender" -ForegroundColor DarkYellow
    cd EmailSender
    ./publish.ps1
    cd ../
    
    Write-Host "Publishing Luogu" -ForegroundColor DarkYellow
    cd Luogu
    ./publish.ps1
    cd ../
}
catch {
    Pop-Location
}