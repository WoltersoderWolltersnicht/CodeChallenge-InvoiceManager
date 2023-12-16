FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /app

COPY ./src/ ./

RUN dotnet restore "InvoiceManager.App/InvoiceManager.App.csproj" -s https://api.nuget.org/v3/index.json --no-cache

RUN dotnet publish InvoiceManager.App/InvoiceManager.App.csproj -c Release -o out -r linux-x64

FROM mcr.microsoft.com/dotnet/runtime:7.0

RUN groupadd -g 666 dotnet \
    && useradd -m -d "/app" -g dotnet -u 666 -s /bin/bash dotnet

RUN sed -i "s|DEFAULT@SECLEVEL=2|DEFAULT@SECLEVEL=1|g" /etc/ssl/openssl.cnf

WORKDIR /app

COPY --from=build-env /app/out .

EXPOSE 8080
CMD ["bash", "-c", "dotnet InvoiceManager.App.dll"]
