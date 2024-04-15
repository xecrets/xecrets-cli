Param ([Parameter(Mandatory=$true)][string]$Version)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.File.Cli\bin\Release\net8.0\publish\win-x64"
try {
    $compress = @{
        Path = "XecretsFileCli.exe"
        DestinationPath = "XecretsFileCli-Win-${Version}.zip"
    }
    Compress-Archive @compress
}
finally
{
    Pop-Location
}
