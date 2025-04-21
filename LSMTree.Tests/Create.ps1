$baseUrl = "http://localhost:5296"
$key = 1
$value = 100

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