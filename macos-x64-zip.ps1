Param ([Parameter(Mandatory=$true)][string]$Version)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.Cli\bin\Release\net8.0\publish\osx-x64"
try {
    wsl chmod 775 XecretsCli
    wsl mkdir XecretsCli-macOS-${Version}
    wsl cp -r XecretsCli XecretsCli-macOS-${Version}
    wsl tar -czvf XecretsCli-macOS-${Version}.tar.gz XecretsCli-macOS-${Version}
    wsl rm -rf XecretsCli-macOS-${Version}
}
finally
{
    Pop-Location
}
