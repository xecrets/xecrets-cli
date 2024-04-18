Param ([Parameter(Mandatory=$true)][string]$Version)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.Cli\bin\Release\net8.0\publish\Linux-x64"
try {
    wsl chmod 775 XecretsCli
    wsl mkdir XecretsCli-Linux-${Version}
    wsl cp -r XecretsCli XecretsCli-Linux-${Version}
    wsl tar -czvf XecretsCli-Linux-${Version}.tar.gz XecretsCli-Linux-${Version}
    wsl rm -rf XecretsCli-Linux-${Version}
}
finally
{
    Pop-Location
}
