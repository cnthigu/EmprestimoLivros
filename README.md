# Emprestimo de livros

Este é o meu primeiro projeto de controle para Emprestimo. Ele utiliza tecnologias como .NET Framework, ASP.NET Core MVC e SQL Server. O objetivo do projeto é fornecer uma interface para gerenciar as atividades de uma Biblioteca, com foco em facilitar a administração.

## Descrição

Este sistema é um CRUD para o gerenciamento de uma biblioteca. Ele foi desenvolvido utilizando o framework ASP.NET Core MVC e banco de dados SQL Server, com uma arquitetura que visa a performance e escalabilidade.

## Instrução de Instalação

### Pré-requisitos

1. **.NET Core SDK** (para rodar o backend).
2. **SQL Server** (para banco de dados).

### Etapas

1. Clone o repositório:
    ```bash
    git clone https://github.com/cnthigu/academiacontrol.git
    ```

2. Configuração do banco de dados:
    - Abra o arquivo `appsettings.json` e coloque suas configurações de conexão com o SQL Server.
    - Caso ainda não tenha o banco de dados configurado, crie um novo banco de dados no SQL Server.

4. Atualização do banco de dados (Gerenciamento de pacotes NuGet):
    - Abra o **Console Gerenciador de Pacotes** do Visual Studio e execute o seguinte comando para aplicar as migrações no banco de dados:
      ```bash
      UPDATE-DATABASE
      ```

## Instrução de Uso

Após seguir os passos de instalação, você pode rodar o projeto localmente com o comando:

```bash
npm run start
