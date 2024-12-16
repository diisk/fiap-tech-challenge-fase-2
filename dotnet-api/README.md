# Tech Challenge Fase 01

Este projeto segue uma adaptação da **Clean Architecture** utilizando **.NET**. O projeto é estruturado em camadas separadas, cada uma com uma responsabilidade clara, como descrito a seguir.

## O Problema

O **Tech Challenge** desta fase consiste no desenvolvimento de um aplicativo que permita o cadastro e a gestão de contatos regionais, com foco na persistência dos dados e qualidade do software.

### Requisitos Funcionais

- **Cadastro de Contatos**: O sistema deve permitir o cadastro de novos contatos, incluindo nome, telefone e e-mail. Cada contato deve ser associado a um **DDD** correspondente à região.
- **Consulta de Contatos**: Implementar uma funcionalidade para consultar e visualizar os contatos cadastrados, com possibilidade de filtrar os contatos pelo DDD da região.
- **Atualização e Exclusão de Contatos**: O sistema deve possibilitar a atualização e exclusão de contatos previamente cadastrados.

### Requisitos Técnicos

- **Persistência de Dados**: Utilizar um banco de dados para armazenar as informações dos contatos. O projeto pode utilizar **Entity Framework Core** ou **Dapper** como ORM para a camada de acesso a dados.
- **Validações**: Implementar validações de dados, como:
  - Validação do formato de e-mail.
  - Validação do formato de telefone.
  - Garantir que campos obrigatórios sejam preenchidos.
- **Testes Unitários**: Desenvolver testes unitários para garantir a qualidade do código, utilizando frameworks como **xUnit** ou **NUnit**.

## Estrutura do Projeto

A arquitetura do projeto foi dividida em quatro camadas principais:

1. **Dominio (Domain)**:  
   Contém as entidades, enumeradores, exceptions customizadas, além das interfaces para os repositórios e serviços de domínio. Esta camada é a mais interna e independente, focada nas regras de negócio.

2. **Aplicação (Application)**:  
   Responsável por gerenciar os casos de uso da aplicação. Aqui estão os DTOs, gateways, mappers e as implementações dos serviços de domínio definidos na camada de domínio.

3. **Infraestrutura (Infrastructure)**:  
   Contém os detalhes de implementação como os **DbContexts**, **configurações de mapeamento de entidades**, **migrations**, além dos repositórios e gateways que interagem com o banco de dados ou outros serviços externos.

4. **API**:  
   A camada mais externa que expõe os serviços da aplicação. Aqui estão os **controllers**, **middlewares**, e o ponto de entrada da aplicação no arquivo `Program.cs`.

## Tecnologias Utilizadas

- **.NET 6**
- **Entity Framework Core**
- **MySQL**
- **Swagger** para documentação da API
- **Insomnia** para testes

## Passos para o Desenvolvimento do Projeto

1. **Event Storming**:  
   Realizei o **Event Storming** para mapear os eventos e comportamentos da aplicação. A imagem está na raiz do projeto API com o nome [EventStorming.png](https://github.com/diisk/TechChallengeContatosRegionais/blob/31376fb9366c2438008785024443a014bc98322b/API/EventStorming.png).

2. **Criação do Projeto**:  
   Utilizei um projeto modelo desenvolvido durante as aulas da primeira fase da pós-graduação, adaptando para a Clean Architecture, com a divisão em camadas mencionada acima.

3. **Configuração do Banco de Dados**:  
   O banco de dados utilizado foi o **MySQL**, integrado com **Entity Framework Core**. Foram configurados dois **DbContexts**, um para leitura e outro para escrita. Além disso, foi criada uma classe base que implementa `IDesignTimeDbContextFactory` para carregar as configurações do `appsettings.json` em tempo de design, facilitando a execução dos comandos de migration.

4. **Criação das Entidades**:  
   As entidades do sistema herdam de uma entidade base que possui:
   - ID
   - Data de criação
   - Data de atualização
   - Flag de remoção lógica
   - Método de validação para validar as annotations das entidades

5. **Repositórios**:  
   Criei repositórios herdando de uma **BaseRepository**, que implementa uma interface genérica `IRepository<T>` para reutilizar métodos comuns. Isso permite reaproveitar as implementações genéricas nos repositórios específicos.
a
6. **Interfaces de Domínio**:  
   Foram criadas interfaces para os serviços de domínio, garantindo a separação de responsabilidades e facilitando a implementação de testes.

7. **Implementação dos Serviços**:  
   Os serviços foram implementados seguindo o **TDD (Test Driven Development)**, garantindo que a lógica de negócio foi testada adequadamente antes da implementação.

8. **Criação da Primeira Migration**:  
   Após configurar as entidades e o banco de dados, criei a primeira **migration** para gerar as tabelas no banco.

9. **Endpoints Implementados**:  
   Implementados os endpoints principais para interação com o sistema através da API.

10. **Cache Implementado**:  
   Foi implementado um sistema de cache, inicialmente apenas no serviço de geração de tokens.

11. **Documentação com Swagger**:  
   A documentação da API foi gerada pelo **Swagger** foi modificada pelo summary, permitindo a visualização e teste dos endpoints.

12. **Testes com Insomnia**:  
   Todos os testes de integração e endpoints foram realizados com **Insomnia**. O arquivo de configuração do Insomnia está na raiz do projeto API com o nome [InsomniaConfig](https://github.com/diisk/TechChallengeContatosRegionais/blob/31376fb9366c2438008785024443a014bc98322b/API/InsomniaConfig).
