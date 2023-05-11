# AspnetMicroservices (implemented by my own)

**Refer the main repository -> https://github.com/aspnetrun/run-aspnetcore-microservices**

See the overall picture of **implementations on microservices with .net tools** on real-world **e-shop microservices** project;

![microservices_remastered](https://user-images.githubusercontent.com/1147445/110304529-c5b70180-800c-11eb-832b-a2751b5bda76.png)

## Run The Project

You will need the following tools:

- [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
- [.Net Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Run below command

```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

### Web UI (make sure that every microservices are healthy)

- Launch: http://host.docker.internal:8013

![mainscreen2](https://user-images.githubusercontent.com/1147445/81381837-08226000-9116-11ea-9489-82645b8dbfc4.png)
