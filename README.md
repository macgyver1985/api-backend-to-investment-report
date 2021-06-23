# API-BACKEND-TO-INVESTMENT-REPORT

# Indice

  - [INTRODUÇÃO](#introdução)
  - [REQUISITOS FUNCIONAIS](#requisitos-funcionais)
    - [Regras para Calculo do IR](#regras-para-calculo-do-ir)
    - [Regras para Calculo do Resgate Antecipado](#regras-para-calculo-do-resgate-antecipado)
  - [REQUISITOS NÃO FUNCIONAIS](#requisitos-não-funcionais)
  - [PROPÓSTA TÉCNICA](#propósta-técnica)
    - [Arquitetura](#arquitetura)
    - [Técnologias](#técnologias)
    - [Detalhamento da Solução](#detalhamento-da-solução)
      - [Estrutura das Pastas](#estrutura-das-pastas)
      - [Pastas X Clean Architecture](#pastas-x-clean-architecture)
  - [AMBIENTES LOCAL](#ambientes-local)
    - [Pré-requisitos](#pré-requisitos)
    - [Repositório](#repositório)
    - [Dependencias](#dependencias)
    - [Compilando Aplicação](#compilando-aplicação)
    - [Docker-Compose](#docker-compose)
    - [Iniciando com Debug](#iniciando-com-debug)
    - [Execução dos Testes](#execução-dos-testes)
  - [LOGS](#logs)
  - [CI/CD](#ci/cd)
  - [AMBIENTE PRODUTIVO](#ambiente-produtivo)

# INTRODUÇÃO

Aqui está sendo proposto um cenário hipotético onde por meio de uma API RESTful os clientes conseguem obter um relatório consolidado dos seus investmentos em renda fixa. 

# REQUISITOS FUNCIONAIS

Cada item da lista deverá conter seu valor unitário, cálculo de IR conforme regra abaixo e valor calculado caso o cliente queira resgatar seu investmento na data atual.

## Regras para Calculo do IR

A rentabilidade é igual ao Valor Total menos Valor Investido

| Tipo de Investmento | Taxa sobre Rentabilidade |
| ------ | ------ |
| Tesouro Direto | 10% |
| LCI | 5% |
| Fundos | 15% |

## Regras para Calculo do Resgate Antecipado

1. Investmento com mais da metade do tempo em custódia: Perde 15% do valor investido.
1. Investmento com até 3 meses para vencer: Perde 6% do valor investido.
1. Outros: Perde 30% do valor investido.

> Também foi subtraído do ''valor de resgate'' o ''valor do IR'' e eventuais ''outras taxas'' vinculadas ao investimento.

# REQUISITOS NÃO FUNCIONAIS

- Usar agum tipo de cache como REDIS para melhorar a performance.
- O cache só pode durar até as zero hora do dia seguinte.
- Os dados devem ser trabalhados em memória não sendo permitido usar qualquer tipo de banco de dados.
- Usar o source abaixo para obter os investmentos:
	- http://www.mocky.io/v2/5e3428203000006b00d9632a
  - http://www.mocky.io/v2/5e3429a33000008c00d96336
  - http://www.mocky.io/v2/5e342ab33000008c00d96342
- O metadados de resposta deve conter os campos abaixo:
```
{
  "valorTotal": 829.68,
  "investmentos": [{
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
- Docker-Compose.
- CI/CD com CircleCI.
- Cloud Heroku.

## Técnologias

- Liguagem de programação c#.
- Asp Net Core 3.1.
- xUnit para testes e relatório de cobertura.

## Detalhamento da Solução

Visando atender de uma melhor forma os aspectos de "organização, manutenibilidade, rastreabilidade, testabilidade, performance e portabilidade" toda estrutura da aplicações foi baseada em Clean Architecture, abaixo diagrama conceitual dessa proposta:

<img src="https://github.com/macgyver1985/api-backend-to-investment-report/blob/master/docs/clean-architecture.png" alt="Clean Architecture" width="600">

### Estrutura das Pastas

```
	├── src                    	# Código Fonte
	|── InvestmentReport.CrossCutting		# Recursos que são usados por todas as camadas da aplicação
```

### Pastas X Clean Architecture

Abaixo tabela que mostra a qual camada da arquitetura que cada pasta pertence:

| Pasta | Camada |
| ------ | ------ |
| InvestmentReport.CrossCutting | Transversal |

# AMBIENTE LOCAL

## Pré-requisitos

Para que a aplicação seja executada corretamente deve ser instalado os recursos abaixo:

- Asp Net Core 3.1
- Docker
- Docker-Compose

## Repositório

```bash
$ git clone https://github.com/macgyver1985/api-backend-to-investment-report.git
$ cd api-backend-to-investment-report
```

## Dependencias

O build dot Asp Net Core 3.1 já restaura as dependencias, mas segue o comando que só restaura as dependencias:

```bash
$ dotnet restore ./src/InvestmentReport.sln
```

## Compilando Aplicação
 
```bash
$ dotnet build ./src/
```

## Docker-Compose

A publicação local irá subir dois containers, um com a api e outro com o redis, veja abaixo:

```bash
$ docker-cmopose up -d --build
```
> Será executada no endereço http://localhost:8080/InvestmentReport

Para derrubar o ambiente local basta executar o comando abaixo:

```bash
$ docker-cmopose down
```

## Iniciando com Debug

Para iniciar o debug é necessário que a aplicação esteja publicada localmente por causa do redis. Para publicar veja o tópico anterior.

No Visual Studios Code, abra a aplicação a partir da pasta raiz "api-backend-to-investment-report" e siga os passos abaixo:

- Selecione a opção Debug conforme passo 1.
- Escolha o perfil de execução conforme passo 2.
- Aperte no play conforme passo 3 ou a tecla F5 do teclado.

<img src="https://github.com/macgyver1985/api-backend-to-investment-report/blob/master/docs/debug-passos.jpg" alt="Exemplo de debug" width="800">

Em seguida é só colocar o break point nos pontos que deseja debugar, veja exemplo abaixo:

<img src="https://github.com/macgyver1985/api-backend-to-investment-report/blob/master/docs/debug-passos-02.jpg" alt="Exemplo de debug" width="800">

> Será executada no endereço http://localhost:5000/InvestmentReport, basta acessar pelo navegador.

## Execução dos Testes

```bash
$ dotnet test ./src/
```

# LOGS

Os logs da aplicação são armazenados em um arquivo txt de nome "logger_yyyy-MM-dd.txt", onde o yyyy-MM-dd é DateTime.UtcNow.ToString("yyyy-MM-dd").

> Localmente, seja via docker-compose ou debug os logs ficam armazenados em ./logs
> Em produção ficam dentro do container da aplicação no diretório /wwwroot/logs

# CI/CD

Toda solução de CI/CD foi desenvolvida pelo CircleCI.

<img src="https://github.com/macgyver1985/api-backend-to-investment-report/blob/master/docs/ci_cd.png" alt="CI/CD" width="800">

O fluxo da pipeline é composto pelos seguinte passos:

1. Clone do repositório.
2. Execução dos testes unitários.
3. Deploy da aplicação junto a Heroku.

> Para mais detalhes veja em ./.circleci/config.yml

Visando a melhor qualidade das entregas foram feitas as seguintes configurações:

- O passo de testes da pipeline é executado para qualquer branch da aplicação após um push;
- A branch master é protegida para receber push;
- Qualquer PR que é aberta para master exige que os testes da branch de origem tenham ocorrido com sucesso.

<img src="https://github.com/macgyver1985/api-backend-to-investment-report/blob/master/docs/pr.jpg" alt="Pull Request" width="800">

# AMBIENTE PRODUTIVO

O ambiente produtivo da aplicação encontra-se na Heroku e a url de acesso a api é https://investiment-report.herokuapp.com/InvestmentReport.
