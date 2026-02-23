$input = [Console]::In.ReadToEnd() | ConvertFrom-Json
$errorName = if ($input.errorName) { $input.errorName } else { "UnknownError" }
$errorMessage = if ($input.errorMessage) { $input.errorMessage } else { "No message" }
$timestamp = (Get-Date).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")

if (-not (Test-Path "logs")) { New-Item -ItemType Directory -Path "logs" | Out-Null }
Add-Content -Path "logs/errors.log" -Value "[$timestamp] ERROR ${errorName}: $errorMessage"
