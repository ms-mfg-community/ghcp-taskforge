$input = [Console]::In.ReadToEnd() | ConvertFrom-Json
$sourceType = if ($input.sourceType) { $input.sourceType } else { "unknown" }
$timestamp = (Get-Date).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")

if (-not (Test-Path "logs")) { New-Item -ItemType Directory -Path "logs" | Out-Null }
Add-Content -Path "logs/session.log" -Value "[$timestamp] Session started (source: $sourceType)"
