Param (
    [Parameter(Mandatory=$true)][string]$Version,
    [Parameter(Mandatory=$true)][string]$Directory,
    [Parameter(Mandatory=$true)][string]$Platform
)

$ErrorActionPreference = 'Stop'
Push-Location -Path "src\Xecrets.Cli\bin\Release\net8.0\publish\$Directory"
try {
    $cmd = @(
        "exec {BASH_XTRACEFD}>&1"
        "set -euox pipefail"
        "chmod 775 XecretsCli"
        "mkdir 'XecretsCli-$Platform-$Version'"
        "cp -r XecretsCli 'XecretsCli-$Platform-$Version'"
        "tar -czvf 'XecretsCli-$Platform-$Version.tar.gz' 'XecretsCli-$Platform-$Version'"
        "rm -rf 'XecretsCli-$Platform-$Version'"
    ) -join " && "
    
    wsl bash -lc "$cmd"

    if ($LASTEXITCODE -ne 0) {
        throw "WSL command failed with exit code $LASTEXITCODE"
    }
}
finally
{
    Pop-Location
}
