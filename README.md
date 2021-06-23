# API-BACKEND-TO-RENTAL-ESTATE

# Indice

  - [Introdução](#introdução)
  - [Requisitos Funcionais](#requisitos-funcionais)
  - [Requisitos Não Funcionais](#requisitos-não-funcionais)
  - [Propósta Técnica](#propósta-técnica)
  - [Detalhamento da Solução](#detalhamento-da-solução)
  - [Configurando Ambientes](#configurando-ambientes)
  - [Executando Aplicação](#executando-aplicação)

# INTRODUÇÃO

Aqui está sendo proposto um cenário hipotético onde por meio de uma API RESTful os clientes conseguem obter um relatório consolidado dos seus investimentos em renda fixa. 

# REQUISITOS FUNCIONAIS

Cada item da lista deverá conter seu valor unitário, cálculo de IR conforme regra abaixo e valor calculado caso o cliente queira resgatar seu investimento na data atual.

## Regras para Calculo do IR

A rentabilidade é igual ao Valor Total menos Valor Investido

| Tipo de Investimento | Taxa sobre Rentabilidade |
| ------ | ------ |
| Tesouro Direto | 10% |
| LCI | 5% |
| Fundos | 15% |

## Regras para Calculo do Resgate Antecipado

1. Investimento com mais da metade do tempo em custódia: Perde 15% do valor investido.
1. Investimento com até 3 meses para vencer: Perde 6% do valor investido.
1. Outros: Perde 30% do valor investido.

# REQUISITOS NÃO FUNCIONAIS

- Usar agum tipo de cache como REDIS para melhorar a performance.
- O cache só pode durar até as zero hora do dia seguinte.
- Os dados devem ser trabalhados em memória não sendo permitido usar qualquer tipo de banco de dados.
- Usar o source abaixo para obter os investimentos:
	- http://www.mocky.io/v2/5e3428203000006b00d9632a
  - http://www.mocky.io/v2/5e3429a33000008c00d96336
  - http://www.mocky.io/v2/5e342ab33000008c00d96342
- O metadados de resposta deve conter os campos abaixo:
```
{
  "valorTotal": 829.68,
  "investimentos": [{
    "nome": "Tesouro Selic 2025",
    "valorInvestido": 799.4720,
    "valorTotal": 829.68,
    "vencimento": "2025-03-01T00:00:00",
    "Ir": 3.0208,
    "valorResgate": 705.228
  }]
}
```

# PROPÓSTA TÉCNICA

## Arquitetura

- Clean Architecture.
- Paradigma Orientado a Objetos.
- SOLID.
- Design Patterns GoF.
- REDIS para cache.
- Docker.
- CI/CD com CircleCI.
- Cloud Heroku.

## Técnologias

- Liguagem de programação c#.
- ASP Net Core 3.1.
- xUnit para testes e relatório de cobertura.

## Detalhamento da Solução

Visando atender de uma melhor forma os aspectos de "organização, manutenibilidade, rastreabilidade, testabilidade, performance e portabilidade" toda estrutura da aplicações foi baseada em Clean Architecture, abaixo diagrama conceitual dessa proposta:

<img src="https://github.com/macgyver1985/api-backend-to-investiment-report/blob/master/docs/clean-architecture.png" alt="Clean Architecture" width="600">

### Estrutura das Pastas

```
	├── src                    	# Código Fonte
	|── InvestimentReport.CrossCutting		# Recursos que são usados por todas as camadas da aplicação
```

### Pastas X Clean Architecture

Abaixo tabela que mostra a qual camada da arquitetura que cada pasta pertence:

| Pasta | Camada |
| ------ | ------ |
| InvestimentReport.CrossCutting | Transversal |

# CONFIGURANDO AMBIENTES

## Pré-requisitos

Para que a aplicação seja executada corretamente deve ser instalado os recursos abaixo:

- ASP Net Core 3.1
- Docker

## Ambiente de Desenvolvimento

### Repositório
```bash
$ git clone https://github.com/macgyver1985/api-backend-to-investiment-report.git
$ cd api-backend-to-investiment-report
```

#### Dependencias

```bash
$ cd src
$ dotnet restore
```

#### Transpilando

Este comando irá converter o código de **typescript** para **javascript** e salvar na pasta **dist**.
 
```bash
$ npm run build
```

#### Iniciando sem Debug

```bash
$ npm start
```

> Será executada no endereço http://localhost:3333/graphql

#### Iniciando com Debug

Basta abrir a pasta "api-backend-to-rental-estate" pelo Visual Studio Code e executar o comando abaixo no terminal:

```bash
$ npm run dev
```
> Será executada no endereço http://localhost:3333/graphql

Em seguida é só colocar o break point nos pontos que deseja debugar, veja exemplo abaixo:

<img src="https://github.com/macgyver1985/api-backend-to-rental-estate/blob/master/docs/debug-example.jpg" alt="Exemplo de debug" width="800">

#### Execução dos Testes

Este comando irá executar os teste e disponibulizar o relatório de cobertura na pasta **coverage**.

```bash
$ npm test
```

## Publicando em Docker

A aplicação está preparada para ser executada em container.

Caso o ***npm run publish*** já tenha sido executado, execute os comandos abaixo:

```bash
$ docker stop -t 0 macgyver_application
$ docker rm macgyver_application
$ docker rmi macgyver1985/imoveis
```

Para publicar no container execute o comando abaixo:

```bash
$ npm run publish
```

> Será executada no endereço http://localhost:3333/graphql

Caso queria subir a imagem da aplicação manualmente execute os comandos abaixo:

```bash
$ npm run build
$ docker build -f Dockerfile -t macgyver1985/imoveis .
$ docker run -d -e NODE_ENV=production -p 3333:3333 macgyver1985/imoveis
```

> Será executada no endereço http://localhost:3333/graphql

# Executando Aplicação

As opções descritas abaixo servem para instânicas da aplicação em ambiente local ou container.

### Usuários

Foram configurados dois usuários, onde uma está atralado ao Portal Imóveis .COM e o outro ao Pronto pra Morar.
Para ter acesso aos imóveis disponíveis a cada portal basta gerar o token JWT com os respectivos usuários.

###### Pronto pra Morar

- Username: **prontopramoraruser**
- Password: **prontopramorarpwd**

###### Imóveis .COM

- Username: **imoveiscomuser**
- Password: **imoveiscompwd**

### Apollo Playground

Excelente forma de efetuar requisições à API pois já fornece toda a documentação dos "SCHEMAS" e ajuda muito na construção das chamadas do tipo "Mutation ou Query".

<img src="https://github.com/macgyver1985/api-backend-to-rental-estate/blob/master/docs/apollo-playground-example.jpg" alt="Apollo Playground" width="800">

##### Obter Token JWT

- Acesso a url http://localhost:3333/graphql
- Execute a mutation GetAuthorization

```
mutation{
  GetAuthorization (
    command: {
      userName: "imoveiscomuser"
      password: "imoveiscompwd"
    }
  ) {
    authorization,
    expiresIn
  }
}
```

<img src="https://github.com/macgyver1985/api-backend-to-rental-estate/blob/master/docs/get-token-example.jpg" alt="Exemplo de Token JWT" width="800">

##### Obter Lista de Imoveis

- Acesso a url http://localhost:3333/graphql
- Execute a query obtainRealEstate informando quais campos deseja receber

```
query {
  obtainRealEstate(
    command:{
      pageNumber: 1,
      pageSize: 10
    }) {
    pageNumber
    pageSize
    totalPages
    totalCount
    listings {
      id
      usableAreas
      bathrooms
      bedrooms
      createdAt
      updatedAt
      listingType
      listingStatus
      parkingSpaces
      owner
      images
      address {
        city
        neighborhood
        geoLocation{
          precision
          location{
            lon
            lat
          }
        }
      }
      pricingInfos {
        businessType
        price
        period
        yearlyIptu
        monthlyCondoFee
        rentalTotalPrice
      }
    }
  }
}
```

- Configura o token em HTTP HEADERS incluindo o parâmetro "authorization"

```
{
  "authorization": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjbGFpbXMiOiJ7XCJ1c2VyTmFtZVwiOlwidml2YXJlYWx1c2VyXCIsXCJ1c2VySWRcIjpcIjI3OTQ5OGM5LTFmYTctNDY5Zi1iNmQ2LWIyYjYwMDY1NDI1NlwifSIsImNyZWF0ZWREYXRlIjoiMjAyMS0wNS0xOFQwNTowNDo0NS42ODRaIiwiZXhwaXJlc0RhdGUiOiIyMDIxLTA1LTE4VDA2OjA0OjQ1LjY4NFoiLCJleHBpcmVzSW4iOjM2MDAwMDAsImlkZW50aXR5IjoiMTQyY2UxZGQtNjRkYS00MGFiLTk0NTgtMGMzZjg4YTVmYTZhIiwiaWF0IjoxNjIxMzE0Mjg1LCJleHAiOjE2MjEzMTc4ODV9.-G_ShzMbwYN5kZgJWoDdc92kIxXuvmJ2ajf93D479QU"
}
```

<img src="https://github.com/macgyver1985/api-backend-to-rental-estate/blob/master/docs/obtain-real-etates-example.jpg" alt="Exemplo de Obter Imoveis" width="800">

