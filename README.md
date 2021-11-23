[![GitHub license](https://img.shields.io/github/license/Naereen/StrapDown.js.svg)](https://github.com/pmarcelojr/PontoPlus/blob/main/LICENSE)
![](/PontoPlus/wwwroot/images/logo1.png)

# 1. Requisitos
## 1.1. Instalar dotnet ef:

Para instalar o dotnet ef local, execute as seguintes instruções:

```
dotnet new tool-manifest
dotnet tool install --local dotnet-ef --version 5.0.5
```


# 2. Banco de dados
## 2.1 Criar instância do MySQL 8 (Docker)
```
docker run --name PontoPlus_DB -e MYSQL_DATABASE=PontoPlus -e MYSQL_ROOT_PASSWORD="123456" -p 3306:3306 -d mysql:8
```
## 2.2. Configurar banco de dados
Alterar o appsettings.json com os dados do seu banco mysql.

## 2.3. Iniciar as Migrations
```
dotnet ef migrations add FirstMigration
dotnet ef database update FirstMigration
```


# 3. Configurar Aplicação
### 3.1. Adicionando o User Secrets
Executar o comando 
```
dotnet user-secret init
```
```
dotnet user-secrets set "dummy" "1234"
```

Procurar navegar até `%appdata%/Microsoft/UserSecrets` e procurar a pasta com o um nome UUID aleatório gerado pelo sistema.
Entre na pasta e edite o arquivo `secrets.json`, adicionando o seguinte conteúdo:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost; port=3306; database=PontoPlus; user=root; password=123456; Persist Security Info=False; Connect Timeout=300"
  }
}
```


# 4. Iniciar
```
dotnet run
```

### 4.1. Iniciar com watch
```
dotnet watch run
```

# 5. Testar disparo de e-mail
O sistema irá retornar uma mensagem com status 200 se o usuário estiver cadastrado, ou 400 caso o endereço de e-mail não tenha sido encontrado.

A hamada para o endpoint deve ser do tipo POST.

Exemplo de requisição via CURL:
```
curl -i -k -X post https://localhost:5001/api/email/redefinir-senha?para=usuario.cadastrado@gmail.com
```

## Contributing

Solicitações Pull Request são bem-vindas. Para mudanças importantes, abra um problema primeiro para discutir o que você gostaria de mudar.

## _License_

[MIT](https://choosealicense.com/licenses/mit/)
