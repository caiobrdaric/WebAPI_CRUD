readme
# Sistema de Registros

O **Sistema de Registros** é uma aplicação ASP.NET Core que utiliza Entity Framework Core para gerenciar operações de CRUD (Criar, Ler, Atualizar, Excluir) para entidades de usuário e tarefa. Este projeto foi configurado para usar um banco de dados em memória, facilitando o desenvolvimento e testes sem a necessidade de um banco de dados físico.

## Estrutura do Projeto

O projeto está organizado em várias pastas principais, cada uma responsável por uma parte específica da aplicação:

### 1. Controllers

Esta pasta contém os controladores da API, que são responsáveis por processar as requisições HTTP e retornar as respostas apropriadas. No projeto, há um controlador principal:

- **UsuarioController**: Este controlador gerencia as operações relacionadas aos usuários. Ele define os endpoints da API que permitem buscar e manipular dados de usuários.

### 2. Data

A pasta Data contém o contexto do banco de dados e as configurações de mapeamento dos modelos. 

- **SistemaTarefasDBContext**: Define o contexto do banco de dados que gerencia as entidades `UserModel` e `TarefaModel`. Configura a utilização do banco de dados em memória e aplica as configurações de mapeamento das entidades.

### 3. Models

Esta pasta contém as classes que representam os dados utilizados na aplicação.

- **UserModel**: Representa um usuário no sistema, com propriedades como CPF, nome e e-mail.

- **TarefaModel**: Representa uma tarefa no sistema, incluindo propriedades para ID, nome, descrição e status. O status é gerenciado por uma enumeração que define os possíveis estados da tarefa.

### 4. Repositories

Esta pasta contém a lógica de acesso a dados, responsável por interagir com o banco de dados.

- **IUsuarioRepositorio**: Define a interface que descreve os métodos para operações de CRUD relacionadas a usuários.

- **UsuarioRepositorio**: Implementa a interface `IUsuarioRepositorio`. Contém a lógica para buscar, adicionar, atualizar e excluir usuários no banco de dados. Utiliza o contexto do banco de dados para realizar essas operações.

### 5. Enums

A pasta Enums contém enumerações usadas no projeto.

- **StatusTarefa**: Enumeração que define os possíveis status de uma tarefa, como "A Fazer", "Em Andamento" e "Concluído".

## Funcionamento

- **Configuração do Banco de Dados**: O `SistemaTarefasDBContext` está configurado para usar um banco de dados em memória. Isso significa que todos os dados são armazenados temporariamente e são perdidos quando a aplicação é encerrada. Essa configuração facilita o desenvolvimento e os testes.

- **Controladores**: Os controladores expõem endpoints da API que permitem interagir com os dados. Eles utilizam os repositórios para realizar operações de CRUD, enviando e recebendo dados em formato JSON.

- **Repositórios**: Encapsulam a lógica de acesso aos dados. Os repositórios utilizam o contexto do banco de dados para executar operações e manipular os dados.

- **Mapeamento e Modelos**: As classes de modelo representam os dados que a aplicação manipula, e as classes de mapeamento definem como essas classes são armazenadas no banco de dados.

## Contribuição

Contribuições para o projeto são bem-vindas. Para contribuir, faça um fork do repositório, crie uma branch para suas alterações e envie um pull request com suas contribuições.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

## Contato

Para dúvidas ou suporte, entre em contato pelo e-mail: [seuemail@exemplo.com](mailto:seuemail@exemplo.com).
