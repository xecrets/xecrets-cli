Param ([Parameter(Mandatory=$true)][string]$Version)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.File.Cli\bin\Release\net7.0\publish\Linux-x64"
try {
    wsl chmod 775 XecretsFileCli
    wsl mkdir XecretsFileCli-Linux-x64-${Version}
    wsl cp -r XecretsFileCli XecretsFileCli-Linux-x64-${Version}
    wsl tar -czvf XecretsFileCli-Linux-x64-${Version}.tar.gz XecretsFileCli-Linux-x64-${Version}
    wsl rm -rf XecretsFileCli-Linux-x64-${Version}
}
finally
{
    Pop-Location
}
