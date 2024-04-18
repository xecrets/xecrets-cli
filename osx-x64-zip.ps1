Param ([Parameter(Mandatory=$true)][string]$Version)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.Cli\bin\Release\net8.0\publish\osx-x64"
try {
    wsl chmod 775 XecretsCli
    wsl mkdir XecretsCli-Osx-${Version}
    wsl cp -r XecretsCli XecretsCli-Osx-${Version}
    wsl tar -czvf XecretsCli-Osx-${Version}.tar.gz XecretsCli-Osx-${Version}
    wsl rm -rf XecretsCli-Osx-${Version}
}
finally
{
    Pop-Location
}
