Param ([Parameter(Mandatory=$true)][string]$Version)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.Cli\bin\Release\net7.0\publish\osx-arm64"
try {
    wsl chmod 775 XecretsCli "&&" tar -czvf XecretsCli-Osx-arm64-${Version}.tar.gz XecretsCli
}
finally
{
    Pop-Location
}
