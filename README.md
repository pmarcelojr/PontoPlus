[![GitHub license](https://img.shields.io/github/license/Naereen/StrapDown.js.svg)](https://github.com/pmarcelojr/PontoPlus/blob/main/LICENSE)

![](/PontoPlus/wwwroot/images/logo1.png)

# _Requisitos_

## 1. Instalar dotnet ef:

Para instalar o dotnet ef local, execute as seguintes instru��es:

```
dotnet new tool-manifest
dotnet tool install --local dotnet-ef --version 5.0.5
```

# Banco de dados

## 1. Criar inst�ncia do MySQL 8 (Docker)

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

### 4.1 Iniciar

```
dotnet run
```

### 4.2 Iniciar com watch

```
dotnet watch run
```

## Contributing

Solicitações Pull Request são bem-vindas. Para mudanças importantes, abra um problema primeiro para discutir o que você gostaria de mudar.

## _License_

[MIT](https://choosealicense.com/licenses/mit/)
