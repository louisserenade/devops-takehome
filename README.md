# DevOps Take-Home Assignment

## Overview

This repository contains two components:

1. **HelloWorld Web Application**
2. **HWMonitor Windows Service**

The Windows Service monitors the IIS-hosted HelloWorld application every 60 seconds.

If the HTTP response is not `200 OK`, the service stops automatically and is configured to restart after 300 seconds.

---

## Project Structure

```
devops-takehome/
├── HelloWorld/
└── HWMonitor/
```

---

## HelloWorld Web Application

- ASP.NET Core 3.1
- Deployed to IIS
- Runs on: http://localhost:5001

Test endpoint:

```
http://localhost:5001/status
```

Expected response:

```
Application is running
```

---

## HWMonitor Windows Service

### Features

- Checks IIS site every 60 seconds
- Logs timestamp + HTTP status code
- Stops service if status != 200
- Auto-restart after 300 seconds (Windows Service recovery)

---

## Run in Development (EDI)

```
dotnet run
```

---

## Publish

```
dotnet publish -c Release -r win-x64 --self-contained true
```

---

## Deploy as Windows Service

Run:

```
.\deploy-service.ps1
```

This script will:

- Create Windows Service
- Set Startup Type to Automatic
- Configure recovery to restart after 300 seconds
- Start the service

---

## Verification

Check service status:

```
Get-Service HWMonitorService
```

Check recovery configuration:

```
sc qfailure HWMonitorService
```

Log file is created in the same folder as the executable.

---

## Author

Thach Duong