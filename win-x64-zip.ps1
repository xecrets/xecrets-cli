Param ([Parameter(Mandatory=$true)][string]$Version)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.Cli\bin\Release\net8.0\publish\win-x64"
try {
    $compress = @{
        Path = "XecretsCli.exe"
        DestinationPath = "XecretsCli-Win-x64-${Version}.zip"
    }
    Compress-Archive @compress
}
finally
{
    Pop-Location
}
