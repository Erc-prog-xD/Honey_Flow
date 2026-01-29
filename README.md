# Honey Flow
O Honey Flow é um sistema de gerenciamento de apiários, colmeias e produção de mel. Esta aplicação foi projetada para oferecer uma experiência de gestão intuitiva e responsiva, permitindo que pequenos e médios apicultores monitorem seus apiários, controlem seus estoques e acompanhem vendas e perdas com facilidade.

O objetivo principal é simplificar o dia a dia da produção melífera, substituindo anotações manuais por uma interface digital que centraliza as informações essenciais para o crescimento do produtor.
Para acessar nosso manual de uso, [clique aqui](https://drive.google.com/file/d/1XrOyurh2RvqMk1Qsp84di_o2amaehKIV/view?usp=sharing) 

## Backend API
Esta API foi construída com foco em escalabilidade, segurança e integridade de dados.
### Tecnologias Utilizadas
O projeto utiliza o ecossistema moderno da Microsoft para garantir alta performance:
* Linguagem: C# 13.0.
* Framework: .NET 9.0 (ASP.NET Core).
* Banco de Dados: Microsoft SQL Server.
* ORM: Entity Framework Core 9.0 (Abordagem Code First).
* Segurança: Autenticação Stateless com JWT (JSON Web Token).
* Documentação: Swagger (OpenAPI 3.0).
### Arquitetura e Padrões
A aplicação segue o padrão MVC (Model-View-Controller) adaptado para Web API, organizado nas seguintes camadas:
* Controllers: Gerenciam as requisições HTTP e validam os dados de entrada.
* Service Layer: Onde reside toda a inteligência e regras de negócio, utilizando interfaces para baixo acoplamento.
* Data/Models: Representam a persistência e a estrutura do banco de dados.

Princípios Aplicados:
* SOLID & DRY: Garantia de código limpo e manutenível.
* Injeção de Dependência: Uso do contêiner nativo do .NET para gerenciar ciclos de vida (Scoped).
* Padrão DTO: Desacoplamento das entidades do banco da interface pública para maior segurança.
* Soft Delete: Exclusão lógica para preservação de dados históricos e auditoria.
### Estrutura de Diretórios Principal
```
BackendApi/
├── Controllers/ # Endpoints da API
├── DTO/ # Objetos de transferência de dados
├── Services/ # Lógica de negócio (Interfaces e Implementações)
├── Models/ # Entidades do domínio
├── Data/ # DbContext e Migrations
└── Enums/ # Tipos enumerados do sistema
```
### Segurança (Ownership Validation)
O sistema implementa uma camada rigorosa de Validação de Propriedade. Através das Claims do token JWT, a API garante que um usuário só possa acessar ou manipular apiários e colmeias que pertençam ao seu próprio ID, mitigando ataques do tipo IDOR.
### Principais Módulos e Endpoints
1. Autenticação (/api/Auth)
    * Registro e Login: Criação de contas com senhas criptografadas (Hash HMACSHA512) e geração de tokens de acesso.
2. Apiários(/api/Apiario)
    * Gerenciamento das unidades produtivas.
    * Destaque: A exclusão de um apiário realiza um Soft Delete em Cascata, desativando automaticamente todas as colmeias vinculadas.
3. Colmeias (/api/Colmeia)
    * Controle individual de colônias (ano da rainha, status e vinculação ao apiário).
4. Movimentação de Mel (/api/Movimentacao)
    * Registro de Colheita, Venda, Perda e Doação.
    * Regra de Negócio: O sistema atualiza automaticamente o estoque consolidado e os totais financeiros do apiário a cada operação.
### Ambiente de Desenvolvimento
* Pré-requisitos:
    * Docker instalado e rodando
* Configuração: As variáveis de ambiente estão definidas em appsettings.Development.json.
* Banco de Dados: As migrações são aplicadas automaticamente ao iniciar a aplicação.
* URL Base: HTTPS: https\://localhost:8080
* Documentação Interativa: Acesse o Swagger em: https\://localhost:8080/swagger/index.html
## Frontend (React + Vite)
A interface de usuário foi construída seguindo padrões modernos de desenvolvimento, focando na componentização, segurança das rotas e uma integração fluida com a API RESTful do sistema.
### Tecnologias Principais
* Vite + React: Ambiente de desenvolvimento de alta performance.
* React Router Dom: Gerenciamento de navegação SPA (Single Page Application).
* CSS Global: Estilização centralizada em assets/css/global.css.
### Integração com Mapas (Geolocalização)
Para a visualização espacial dos apiários, o sistema utiliza a biblioteca React Leaflet. Esta integração permite:
* Visualização Dinâmica: Renderização de mapas interativos baseados em dados do OpenStreetMap.
* Geolocalização de Apiários: Plotagem de marcadores (Markers) no mapa utilizando as coordenadas (Latitude e Longitude) armazenadas no backend.
* Interface Intuitiva: Facilita a identificação geográfica de cada unidade produtiva diretamente pelo dashboard.
### Segurança e Rotas
A aplicação implementa um componente de ProtectedRoute, que garante que apenas usuários com um Token JWT válido acessem as áreas de gestão.
Fluxo de Navegação:
* Público: Login, Recuperação e Reset de Senha.
* Privado: Dashboard, Cadastros e Relatórios Financeiros/Produção


***Nota:***  *Por padrão, o Vite roda na porta 5173. O backend já está configurado para aceitar requisições desta origem específica.*
## Execução com Docker
O Honey Flow está totalmente conteinerizado, o que garante que a aplicação funcione de forma idêntica em qualquer ambiente, sem a necessidade de instalar dependências locais como o SQL Server ou o SDK do .NET.

### Pré-requisitos
* Docker Desktop instalado e rodando.
* Docker Compose habilitado.
### Como subir a aplicação
Execute os comandos a baixo no terminal da sua escolha que esteja com o git habilitado.
```
git clone https://github.com/Erc-prog-xD/Honey_Flow.git
```
```
cd Honey_Flow
```
Na raiz do projeto, execute o seguinte comando no terminal:
```
docker-compose up -d --build
```

### O que acontece durante esse processo?
O Docker Compose irá orquestrar três serviços principais:
* db: Sobe uma instância do SQL Server. As tabelas são criadas automaticamente através das Migrations na inicialização do backend.
* backend: Compila a API em .NET 9.0 e a expõe na porta 8080.
* Swagger: Disponível em http\://localhost:8080/swagger
* frontend: Inicia o servidor Vite com o React, expondo a interface na porta 5173.
### Variáveis de Ambiente (Docker)
Caso precise ajustar as portas ou credenciais, verifique o arquivo .env na raiz ou as configurações no docker-compose.yml:
* DB\_PASSWORD: Senha do SA do SQL Server.
* JWT\_KEY: Chave de segurança para geração dos tokens.
* VITE\_API\_URL: URL de comunicação do frontend com a API.
### Como parar a execução
Para encerrar todos os serviços e remover os contêineres:
```
docker-compose down
```
