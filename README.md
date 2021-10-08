[![GitHub license](https://img.shields.io/github/license/Naereen/StrapDown.js.svg)](https://github.com/pmarcelojr/PontoPlus/blob/main/LICENSE)

# Requisitos
## 1. Instalar dotnet ef:
Para instalar o dotnet ef local, execute as seguintes instruções:
```
dotnet new tool-manifest 
dotnet tool install --local dotnet-ef --version 5.0.5
```

# Banco de dados
## 1. Criar instância do MySQL 8 (Docker)
```
docker run --name PontoPlus_DB -e MYSQL_DATABASE=PontoPlus -e MYSQL_ROOT_PASSWORD="123456" -p 3306:3306 -d mysql:8
```
## 2. Configurar banco de dados
Alterar o appsettings.json com os dados do seu banco mysql.

## 3. Iniciar as Migrations
```
dotnet ef migrations add FirstMigration
dotnet ef database update FirstMigration
```

## 4. Rodar o projeto 
```
dotnet run
```

![](/PontoPlus/wwwroot/images/logo1.png)
