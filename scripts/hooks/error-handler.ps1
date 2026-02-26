$input = [Console]::In.ReadToEnd() | ConvertFrom-Json
$errorMessage = if ($input.error -and $input.error.message) { $input.error.message } else { "No message" }
$errorCode = if ($input.error -and $input.error.code) { $input.error.code } else { "UnknownError" }
$timestamp = (Get-Date).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")

if (-not (Test-Path "logs")) { New-Item -ItemType Directory -Path "logs" | Out-Null }
Add-Content -Path "logs/errors.log" -Value "[$timestamp] ERROR ${errorCode}: $errorMessage"
