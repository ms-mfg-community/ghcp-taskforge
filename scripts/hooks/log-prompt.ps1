$input = [Console]::In.ReadToEnd() | ConvertFrom-Json
$prompt = if ($input.prompt) { $input.prompt } else { "empty" }
$timestamp = (Get-Date).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")

# Redact potential secrets (long alphanumeric strings that may be tokens/keys)
$prompt = $prompt -replace '[A-Za-z0-9_\-]{20,}', '[REDACTED]'
# Truncate to first 200 chars to limit sensitive data exposure
if ($prompt.Length -gt 200) { $prompt = $prompt.Substring(0, 200) }

if (-not (Test-Path "logs")) { New-Item -ItemType Directory -Path "logs" | Out-Null }
Add-Content -Path "logs/prompts.log" -Value "[$timestamp] Prompt: $prompt"
