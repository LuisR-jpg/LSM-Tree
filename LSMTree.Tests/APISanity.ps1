$baseUrl = "http://localhost:5296"
$key = 1
$value = 100

function Print-Response($label, $response) {
    Write-Host "`n[$label]" -ForegroundColor Cyan
    $response | ConvertTo-Json -Depth 10
}

# --- /health ---
try {
    $health = Invoke-RestMethod -Uri "$baseUrl/health"
    Print-Response "Health Check" $health
} catch {
    Write-Host "Health check failed: $($_.Exception.Message)" -ForegroundColor Red
}

# --- /create ---
try {
    $createBody = @{ key = $key; value = $value } | ConvertTo-Json
    $createResp = Invoke-RestMethod -Uri "$baseUrl/create" `
        -Method POST `
        -Body $createBody `
        -ContentType "application/json"
    Print-Response "Create Entry" $createResp
} catch {
    Write-Host "Create failed: $($_.Exception.Message)" -ForegroundColor Red
}

# --- /read/{key} ---
try {
    $readResp = Invoke-RestMethod -Uri "$baseUrl/read/$key"
    Print-Response "Read Key $key" $readResp
} catch {
    Write-Host "Read failed: $($_.Exception.Message)" -ForegroundColor Red
}

# --- /read/{non-existent key} ---
$fakeKey = 999
try {
    $missing = Invoke-RestMethod -Uri "$baseUrl/read/$fakeKey"
    Print-Response "Read Non-existent Key $fakeKey" $missing
} catch {
    Write-Host "`n[Read Non-existent Key $fakeKey]" -ForegroundColor Cyan
    Write-Host "Expected 404: $($_.Exception.Message)" -ForegroundColor Yellow
}
