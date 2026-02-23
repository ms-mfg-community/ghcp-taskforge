$input = [Console]::In.ReadToEnd() | ConvertFrom-Json
$toolName = if ($input.toolName) { $input.toolName } else { "unknown" }
$resultType = if ($input.toolResult.type) { $input.toolResult.type } else { "unknown" }
$timestamp = (Get-Date).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")

if (-not (Test-Path "logs")) { New-Item -ItemType Directory -Path "logs" | Out-Null }

# Add CSV header if file doesn't exist
if (-not (Test-Path "logs/tool-stats.csv")) {
    Set-Content -Path "logs/tool-stats.csv" -Value "timestamp,tool_name,result_type"
}

Add-Content -Path "logs/tool-stats.csv" -Value "$timestamp,$toolName,$resultType"
