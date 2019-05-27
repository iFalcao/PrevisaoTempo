# Previsão do Tempo App

[![N|Solid](http://files.softicons.com/download/web-icons/vector-stylish-weather-icons-by-bartosz-kaszubowski/png/256x256/sun.big.cloud.drizzle.png)](https://nodesource.com/products/nsolid)

Esse aplicativo possui um back-end em .NET Core 2.2 Web Api e o front-end em Angular 7.

---------

[Clique aqui](https://github.com/iFalcao/PrevisaoTempo/tree/architecture/APIPrevisaoTempo) para ver mais sobre o back-end.

[Clique aqui](https://github.com/iFalcao/PrevisaoTempo/tree/architecture/SitePrevisaoTempo) para ver mais sobre o front-end.

## Rodando o projeto

Passos iniciais:

1. Verifique se possui o .NET Core 2.2 SDK instalado, caso não possua deve baixar através desse [link](https://dotnet.microsoft.com/download/dotnet-core/2.2).

- você pode verificar se possui o .net core instalado com o comando `dotnet --version`

2. Verifique se possui o NodeJS instalado, caso não possua deve baixar através desse [link](https://nodejs.org/en/download/).

- você pode verificar se possui o node instalado (e consequentemente o NPM) com o comando `npm -v`

3. Abra um terminal na pasta que deseja clonar o projeto e rode o seguinte comando: `git clone https://github.com/iFalcao/PrevisaoTempo` .

Agora que você tem tudo baixado localmente, vamos rodar ambos os projetos. Para isso você deve abrir 2 instâncias de terminais diferentes (vamos supor que ambas estejam no root do projeto inicialmente).

#### Rodando a API

Para rodar a API, execute os seguintes comandos:

```terminal
$ cd APIPrevisaoTempo
$ dotnet run watch
```

- O watch serve para que qualquer alteração realizada na API execute um reload e seja rodado o projeto atualizado

#### Rodando o Site

Para rodar o Site, execute os seguintes comandos:

```terminal
$ cd SitePrevisaoTempo
$ npm install
$ npm start
```

- Caso você possua o AngularCLI instalado (pode instalar através do comando `npm install -g @angular/cli`), pode também rodar o comando `ng serve` ao invés de `npm start`

Pronto! Agora você pode abrir o site em http://localhost:4200
Caso deseje visualizar a página do swagger referente a API, navegue para http://localhost:5000/swagger
