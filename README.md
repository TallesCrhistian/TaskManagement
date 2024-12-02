# TaskManagement

Este projeto é uma API construída em .NET. Este guia descreve os passos necessários para clonar o projeto, construir e executar a aplicação.
## Pré-requisitos

Antes de iniciar, certifique-se de que você possui os seguintes itens instalados:

1. [Git](https://git-scm.com/) - Para clonar o repositório.
2. [Docker](https://docs.docker.com/get-docker/) - Docker Desktop para Windows/Mac ou Docker Engine para Linux.
3. [Docker Compose](https://docs.docker.com/compose/install/) - Para orquestrar os containers.

### Verificando Instalações

- **Verificar instalação do Git**:
    ```bash
    git --version
    ```

- **Verificar instalação do Docker**:
    ```bash
    docker --version
    ```

- **Verificar instalação do Docker Compose**:
    ```bash
    docker-compose --version
    ```

Se esses comandos retornarem suas respectivas versões, você está pronto para prosseguir.

## Passos para Executar a Aplicação

### 1. Clonar o Repositório

Clone o repositório para a sua máquina local:

```bash
git clone https://github.com/TallesCrhistian/Auditoria.git
cd auditoria-api
````

### 2. Executar o Docker Compose

```bash
docker-compose up --build
````
Esse comando irá:

. Subir o container do banco de dados SQLServer.

### 3. Instale o SDK do .NET 8 

. https://dotnet.microsoft.com/download

### 4. Baixar Visual Studio 2022.

.https://visualstudio.microsoft.com/pt-br/downloads/

### 5. Adicionar vários projetos de inicialização

- TaskManagement.API
- TaskManagement.UI

### 6. Instale Dependências

```bash
dotnet restore
```

### 7. Compile o projeto

```bash
dotnet build
```
### 8. Execute os projetos

```bash
dotnet run --project TaskManagement.API
dotnet run --project TaskManagement.UI
```

### 9. Execute os projetos

- Informações do banco de dados está no docker-compose.yml


### Resolução de Problemas

### 1. Problemas de Permissão com Volumes
Se você encontrar problemas relacionados a permissões de diretórios nos volumes persistentes, como:

```bash
permission denied
```

Isso pode ser resolvido executando o Docker com permissões de superusuário (no Linux), ou ajustando as permissões dos volumes locais.

### No Linux, tente:

```bash
sudo docker-compose up
```

### 2. Erro de Porta Já em Uso
Se a porta 8080 já estiver em uso, você pode alterar a configuração de portas no arquivo docker-compose.yml. Exemplo de alteração:

```yaml
ports:
  - "8081:80"
```

Isso fará com que a aplicação seja executada na porta 8081 ao invés de 8080.

### 3. Aviso de Proteção de Dados no ASP.NET Core
Se você receber um aviso como este:

```bash
warn: Microsoft.AspNetCore.DataProtection.Repositories.FileSystemXmlRepository[60]
Storing keys in a directory '/home/app/.aspnet/DataProtection-Keys' that may not be persisted outside of the container.
``` 
Isso significa que as chaves de proteção de dados não são persistidas. Para resolver isso, você pode configurar um volume para armazenar essas chaves fora do container.

### Comandos Úteis do Docker
Aqui estão alguns comandos úteis para gerenciar seus containers:

### Ver logs dos containers:

```bash
docker-compose logs -f
```
### Reconstruir a imagem sem cache:

```bash
docker-compose build --no-cache
```

### Verificar status dos containers:

```bash
docker-compose ps
```
