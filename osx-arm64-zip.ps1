Param ([Parameter(Mandatory=$true)][string]$Version)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.File.Cli\bin\Release\net7.0\publish\osx-arm64"
try {
    wsl chmod 775 XecretsFileCli "&&" tar -czvf XecretsFileCli-Osx-arm64-${Version}.tar.gz XecretsFileCli
}
finally
{
    Pop-Location
}
