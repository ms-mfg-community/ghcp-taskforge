$input = [Console]::In.ReadToEnd() | ConvertFrom-Json
$prompt = if ($input.userPrompt) { $input.userPrompt } else { "empty" }
$timestamp = (Get-Date).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")

if (-not (Test-Path "logs")) { New-Item -ItemType Directory -Path "logs" | Out-Null }
Add-Content -Path "logs/prompts.log" -Value "[$timestamp] Prompt: $prompt"
