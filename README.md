# Sistema de Registros

O **Sistema de Registros** é uma aplicação ASP.NET Core que utiliza Entity Framework Core para gerenciar operações de CRUD (Criar, Ler, Atualizar, Excluir) para entidades de usuário. Este projeto foi configurado para usar um banco de dados em memória, facilitando o desenvolvimento e testes sem a necessidade de um banco de dados físico.

## Estrutura do Projeto

O projeto está organizado em várias pastas principais, cada uma responsável por uma parte específica da aplicação:

### 1. Controllers

Esta pasta contém os controladores da API, que são responsáveis por processar as requisições HTTP e retornar as respostas apropriadas. No projeto, há um controlador principal:

- **UsuarioController**: Este controlador gerencia as operações relacionadas aos usuários. Ele define os endpoints da API que permitem buscar e manipular dados de usuários.

### 2. Data

A pasta Data contém o contexto do banco de dados e as configurações de mapeamento dos modelos. 

- **SistemaTarefasDBContext**: Define o contexto do banco de dados que gerencia a entidade `UserModel`. Configura a utilização do banco de dados em memória e aplica as configurações de mapeamento da entidade.

### 3. Models

Esta pasta contém as classes que representam os dados utilizados na aplicação.

- **UserModel**: Representa um usuário no sistema, com propriedades como CPF, nome e e-mail.

### 4. Repositories

Esta pasta contém a lógica de acesso a dados, responsável por interagir com o banco de dados.

- **IUsuarioRepositorio**: Define a interface que descreve os métodos para operações de CRUD relacionadas a usuários.

- **UsuarioRepositorio**: Implementa a interface `IUsuarioRepositorio`. Contém a lógica para buscar, adicionar, atualizar e excluir usuários no banco de dados. Utiliza o contexto do banco de dados para realizar essas operações.

### 5. Tecnologias utilizadas

- .NET 8.0
- C#
- Entity Famework inMemory
- xUnit
- Coverlet.msbuild

## Funcionamento

- **Configuração do Banco de Dados**: O `SistemaTarefasDBContext` está configurado para usar um banco de dados em memória. Isso significa que todos os dados são armazenados temporariamente e são perdidos quando a aplicação é encerrada. Essa configuração facilita o desenvolvimento e os testes.

- **Controladores**: Os controladores expõem endpoints da API que permitem interagir com os dados. Eles utilizam os repositórios para realizar operações de CRUD, enviando e recebendo dados em formato JSON.

- **Repositórios**: Encapsulam a lógica de acesso aos dados. Os repositórios utilizam o contexto do banco de dados para executar operações e manipular os dados.

- **Mapeamento e Modelos**: As classes de modelo representam os dados que a aplicação manipula, e as classes de mapeamento definem como essas classes são armazenadas no banco de dados.

# Relatório percentual de testes.
![Captura de tela 2024-09-18 023307](https://github.com/user-attachments/assets/29569c20-8b03-4772-897d-db7e55c40cb1)
