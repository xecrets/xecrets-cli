Param ([Parameter(Mandatory=$true)][string]$Version)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.File.Cli\bin\Release\net8.0\publish\Linux-x64"
try {
    wsl chmod 775 XecretsFileCli
    wsl mkdir XecretsFileCli-Linux-${Version}
    wsl cp -r XecretsFileCli XecretsFileCli-Linux-${Version}
    wsl tar -czvf XecretsFileCli-Linux-${Version}.tar.gz XecretsFileCli-Linux-${Version}
    wsl rm -rf XecretsFileCli-Linux-${Version}
}
finally
{
    Pop-Location
}
