## About

This solution contains two projects: 

The first is a simple console application that sends an e-mail using Google's SMTP Server.

Second one is a Test Project that simulates sending an email using a Fake SMTP Server and demonstrating how it can be applied and used in Integration Tests.


## Installation and Usage
```bash
git clone https://github.com/rafbaptista/FakeSmtpServer.git
cd FakeSmtpServerDemo
```
## Examples
```bash
### To send a real email (remember to update the environment variables with your credentials)
dotnet run --project SmtpDemo/SmtpDemo.csproj
```

```bash
### Run unit tests simulating sending an e-mail using a Fake SMTP Server and storing tbe email on disk 
dotnet test SmtpDemoTests/SmtpDemoTests.csproj --filter DisplayName~DiskEmailIntegrationTests
```

```bash
### Run unit tests simulating sending an e-mail using a Fake SMTP Server and storing tbe email in memory 
dotnet test SmtpDemoTests/SmtpDemoTests.csproj --filter DisplayName~InMemoryEmailIntegrationTests
```