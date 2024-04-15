Param ([Parameter(Mandatory=$true)][string]$Version)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.File.Cli\bin\Release\net8.0\publish\osx-x64"
try {
    wsl chmod 775 XecretsFileCli
    wsl mkdir XecretsFileCli-Osx-${Version}
    wsl cp -r XecretsFileCli XecretsFileCli-Osx-${Version}
    wsl tar -czvf XecretsFileCli-Osx-${Version}.tar.gz XecretsFileCli-Osx-${Version}
    wsl rm -rf XecretsFileCli-Osx-${Version}
}
finally
{
    Pop-Location
}
