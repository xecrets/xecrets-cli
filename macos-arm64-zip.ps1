Param ([Parameter(Mandatory=$true)][string]$Version)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.Cli\bin\Release\net8.0\publish\osx-arm64"
try {
    wsl chmod 775 XecretsCli
    wsl mkdir XecretsCli-macOS-arm64-${Version}
    wsl cp -r XecretsCli XecretsCli-macOS-arm64-${Version}
    wsl tar -czvf XecretsCli-macOS-arm64-${Version}.tar.gz XecretsCli-macOS-arm64-${Version}
    wsl rm -rf XecretsCli-macOS-arm64-${Version}
}
finally
{
    Pop-Location
}
