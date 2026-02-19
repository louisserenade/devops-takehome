$serviceName = "HWMonitorService"
$displayName = "HelloWorld Monitor Service"
$exePath = "C:\deploy\HWMonitor\HWMonitor.exe"
$user = "NT AUTHORITY\LocalService"
$password = $null

if (Get-Service -Name $serviceName -ErrorAction SilentlyContinue) {
    Stop-Service $serviceName -Force
    sc.exe delete $serviceName
    Start-Sleep -Seconds 2
}


sc.exe create $serviceName `
    binPath= "`"$exePath`"" `
    start= auto `
    obj= "$user"

sc.exe failure $serviceName reset= 0 actions= restart/300000

Start-Service $serviceName

Write-Host "Service deployed and started successfully."
