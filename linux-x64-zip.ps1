Param ([Parameter(Mandatory=$true)][string]$Version)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.Cli\bin\Release\net8.0\publish\linux-x64"
try {
    wsl chmod 775 XecretsCli
    wsl mkdir XecretsCli-Linux-x64-${Version}
    wsl cp -r XecretsCli XecretsCli-Linux-x64-${Version}
    wsl tar -czvf XecretsCli-Linux-x64-${Version}.tar.gz XecretsCli-Linux-x64-${Version}
    wsl rm -rf XecretsCli-Linux-x64-${Version}
}
finally
{
    Pop-Location
}
