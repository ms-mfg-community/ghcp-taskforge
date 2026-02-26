$input = [Console]::In.ReadToEnd() | ConvertFrom-Json
$toolName = $input.toolName

# Check for dangerous bash commands
if ($toolName -eq "bash" -or $toolName -eq "terminal") {
    $command = $input.toolInput.command
    if ($command -match 'rm\s+-[rf]{1,2}\s*/|DROP\s+TABLE|format\s+|sudo\s+rm') {
        Write-Output '{"permissionDecision":"deny","reason":"Blocked dangerous command: potential destructive operation detected"}'
        exit 0
    }
}

# Check for edits outside allowed directories
if ($toolName -eq "edit" -or $toolName -eq "create") {
    $filePath = if ($input.toolInput.path) { $input.toolInput.path } else { $input.toolInput.file }
    if ($filePath -and $filePath -notmatch '(^|[\\/])src[\\/]|(^|[\\/])test[\\/]') {
        Write-Output '{"permissionDecision":"deny","reason":"Blocked edit: file is outside allowed src/ and test/ directories"}'
        exit 0
    }
}

# Allow by default
Write-Output '{"permissionDecision":"allow"}'
