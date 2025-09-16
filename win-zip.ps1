Param (
    [Parameter(Mandatory=$true)][string]$Version,
    [Parameter(Mandatory=$true)][string]$Directory,
    [Parameter(Mandatory=$true)][string]$Platform
)

$workdir = (Get-Location)
Push-Location -Path "src\Xecrets.Cli\bin\Release\net8.0\publish\$Directory"
try {
    $compress = @{
        Path = "XecretsCli.exe"
        DestinationPath = "XecretsCli-$Platform-$Version.zip"
    }
    Compress-Archive @compress
}
finally
{
    Pop-Location
}
