# API do App de Previsão do Tempo 

Esta é a api do projeto, feita em .NET Core 2.2 Web Api seguindo o DDD (Domain Driven Design), com realização de Unit Testing utilizando xUnit e Moq.

---

### Arquitetura

O DDD (Domain Driven Design) é uma modelagem de software cujo objetivo é facilitar a implementação de regras e processos complexos, onde visa a divisão de responsabilidades por camadas e é independente da tecnologia utilizada.

A estruturação de arquivos foi feita da seguinte forma:
 - No root da solution utilizei algumas solution folders para melhor organização, principalmente porque a camada de infraestrutura possui 2 projetos. Desse modo podemos ver claramente a divisão das camadas. 
 1. **Application**
    - A camada de aplicação (Service Layer Pattern) fornece um conjunto de serviços de aplicação responsáveis pela comunicação das requisições de clientes com as demais camadas (no caso desta aplicação, as requisição são feitas através dos endpoints da API).
 2. **Domain**
    - Camada onde residem os objetos de negócio.
 3. **InfraStructure** : é dividida em duas sub-camadas
    - **Data**: realiza a persistência com o banco de dados, no nosso caso utilizando EF Core & SQLite. Utilização do Repository Pattern para prover abstração do tratamento de dados.
    - **Cross-Cutting**: uma camada a parte que não obedece a hierarquia de camada. Como o próprio nome diz, essa camada cruza toda a hierarquia. Responsável por consumir a API externa (OpenWeather).

---

### Testes

Estou utilizando **xUnit** para realizar testes unitários em todas as camadas do nosso projeto. Os testes estão armazenados no projeto **APIPrevisaoTempo.UnitTests** e distribuídos em pastas com o nome da camada destes testes.

Para  executar os testes você deve:
1. Navegar para o root da solution
2. Rodar o comando `dotnet test`


##### Mocking

Mocking em testes unitários é uma tarefa inportante e portanto estou utilizando o pacote **Moq**.

* Para testes unitários da camada de repositório estou utilizando uma database em memória fornecida pelo próprio **Entity Framework Core**.

---

### Banco de Dados

A API utiliza um banco de dados **SQLite** que conta apenas com a tabela **City**.