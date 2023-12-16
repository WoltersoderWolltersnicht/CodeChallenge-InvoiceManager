# Invoice Manager

Test Project to display my knolege in C# using Asp.Net Core, Entity Framework Core, MySQL.

## Startup 

### Using docker-compose.

```
docker-compose build
```

```
docker-compose up
```

### Using docker
Only executes the api for connecting to external MySQL databases

- Edit the [appsettings.json](/src/InvoiceManager.App/appsettings.json) file to match your database configuration.

```
docker build -t invoicemanager .
```

```
docker run -it invoicemanager
```

## Querring

You can get all querys executing the api and using [swagger endpoint](http://localhost:8080/swagger) http://{yourhost}/swagger
