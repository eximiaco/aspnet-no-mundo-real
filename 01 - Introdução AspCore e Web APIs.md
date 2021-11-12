# Introdução ao .NET

A necessidade de construir ou modernizar sistemas de softwares é cada vez maior e a variação de tipos de aplicações que podemos construir é enorme. Hoje, quando pensamos em sistemas de software, não ficamos limitados a aplicativos web, desktop e embarcados! Ainda que estes tipos de aplicações são os mais comuns, não podemos deixar de comentar sobre outras opções:
- Web APIs
- Serveless Functions
- Mobile Apps
- Games
- IoT
- Machine Learning
- Console Apps / Deamons
- Windows Services

Para desenvolver qualquer tipo destas aplicações podemos utilizar a plataforma de desenvolvimento open-source .NET. Além de suportar o desenvolvimento de múltiplos tipos de aplicação podemos desenvolver aplicativos *cross platform* ou seja, para vários sistemas operacionais. 

Dentro da plataforma .NET podemos escolher entre 3 linguagens de programação (C#, F# e VB), além de várias opções de IDEs (*Integrated development environments*). 

## SDK e Runtime

Contudo para iniciarmos o desenvolvimento de uma aplicação em .NET, além da IDE é necessário resalizar o [download de um SDK](https://dotnet.microsoft.com/download) que irá prover suporte para compiladores das linguagens, motor de build, runtimes para asp.net e desktop além de ferramentas CLI (*command-line*) para apoiar o desenvolvimento. Ao criarmos um projeto em .NET (.csproj, .fsproj, .vbproj), este arquivo, esfecifica qual versão do *framework* e SDK vai ser utilizado para que o projeto possa ser compilado e publicado. Os exemplos abaixo ilustram uma configuração de projeto para *Console App* e *Web App*.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>
</Project>
```
Para um projeto Web:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>
</Project>
```

Nos exemplos acima o atributo SDK especifica para o *MSBuild* coo realizar o *build* do projeto. O elemento *TargetFramework* especifica qual versão do .NET framework o aplicativo depende para funcionar.

Uma vez que a aplicação foi desenvolvida, utilizando um SDK .NET podemos gerar um *build* da aplicação e distribuir este artefato para nossos clientes. Para que este artefato possa ser executado, é necessário que os clientes instalem o *runtime* do .NET. Sem o *runtime* artefatos gerados pelo SDK .NET não são suportados pelos *OS* (Sistemas operacionais).

## *Target Framework*

Escolher um framework significa especificar um conjunto de APIs que estarão disponíveis para o aplicativo. Outro aspeceto importante na escolha do framework é o suporte para *cross-plataform*, ou seja, por exemplo ao optar por desenvolver um aplicativo com *.NET Framework 4.8* estamos restrigindo a distribuição do aplicativo para apenas o sistema operacional *Windows*.

Abaixo segue uma tabela com os frameworks mais comuns.

| Target framework | Latest stable version | Target framework moniker (TFM) | .NET Standard version |
|------------------|-----------------------|--------------------------------|-----------------------|
| .NET 5           |          5.0          |             net5.0             |          N/A          |
| .NET Standard    |          2.1          |         netstandard2.1         |          N/A          |
| .NET Core        |          3.1          |          netcoreapp3.1         |          2.1          |
| .NET Framework   |          4.8          |              net48             |          2.0          |

## *.NET Standard*

A fim de trazer uniformidade dentro do ecosistema .NET, a especificação *.NET Standard* nasceu. Seu objetivo é oferecer uma especificação única para múltiplas implmentações do .NET.

Por exemplo um projeto que gera uma biblioteca desenvolvida em *.NET Standard 1.3* pode ser utilizado em outros projetos de *.NET Frameworks* diferentes, desde que suportem a versão em questão. Ou seja, essa biblioteca poderia ser usada em um projeto com *.NET Framework 4.6* e *.NET Core 1.0*, pois ambos *frameworks* implemetam a versão 1.3 do *.NET Standard*.

Para uma visão completa do suporte do .NET Standard, podemos consultar a [documentação online](https://docs.microsoft.com/en-us/dotnet/standard/net-standard).

A imagem abaixo ilustra a idéia por trás do *.NET Standard*.

![.net standard](https://docs.microsoft.com/en-us/dotnet/standard/library-guidance/media/cross-platform-targeting/platforms-netstandard.png)

### .NET Standard e .NET

Existe alguns problemas ao compartilhar código entre plataformas usando *.NET Standard*, como tempo para que uma API fique disponível dentro do *framework*, versionamento complexo, abstração de exceções no uso de APIs não suportadas em uma pltaforma. 

Por estes motivos, o *framework .NET 5* é a forma de unificar em um único produto todas as APIs para desenvolver aplicações *windows desktop*, *cross-plataform console apps*, serviços de nuvem e *websites*. 

Para projetos que estão utilizando o *.NET Standard*, não existe necessidade de alteração, pois o .NET 5 implementa o *.NET Standard 2.1*. Contudo, se precisarmos acessar novas funcionalidades do *runtime, features* de linguagem ou APIs então devemos focar no .NET 5. Por exemplo, para usar C# 9 precisamos apontar o *framework* para .NET 5.

Importante lembrar que o .NET Standard **não foi descontinuado**! Ele ainda é necessário para várias implementações de *frameworks* .NET. Então é recomendado seu uso nos seguintes cenários:

- Usar *.NET Standard 2.0* para compartilhar código entre *.NET Framework 4.x* e outras implementações do .NET;
- Usar *.NET Standard 2.1* para compartilhar código entre Mono, Xamarin e .NET Core.

# Introdução ao ASP.NET Core

ASP.NET Core é um *framework*, *open-source* e *cross-plataform* para construir aplicações modernas com base na nuvem e conectados à internet, como web apps, IoT apps, web APIs e mobile APIs.

Muitos desenvolvedores conhecem ou já usaram o *framework* [ASP.NET 4.x](https://docs.microsoft.com/en-us/aspnet/overview) e conhecem as restrições e problemas da versão como dependencia do *Windows* e *performance*. Contudo o [ASP.NET Core](https://github.com/dotnet/aspnetcore) se trata de um *re-design* da versão 4.x, incluindo alterações arquiteturais para otimizar a execução de aplicativos na nuvem e *on-premises*, componentes modulares para flexibilzar a construção de aplicativos e permitir que possamos desenvolver e executar aplicações *cross-platform* (Windows, Max e Linux).

| ASP.NET Core                                                                                                           | ASP.NET 4.x                                                  |
|------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------|
| Build for Windows, macOS, or Linux                                                                                     | Build for Windows                                            |
| Razor Pages is the recommended approach to create a Web UI as of ASP.NET Core 2.x. See also MVC, Web API, and SignalR. | Use Web Forms, SignalR, MVC, Web API, WebHooks, or Web Pages |
| Multiple versions per machine                                                                                          | One version per machine                                      |
| Develop with Visual Studio, Visual Studio for Mac, or Visual Studio Code using C# or F#                                | Develop with Visual Studio using C#, VB, or F#               |
| Higher performance than ASP.NET 4.x                                                                                    | Good performance                                             |
| Use .NET Core runtime                                                                                                  | Use .NET Framework runtime                                   |

# Meu primeiro projeto ASP.NET Core

1) Verificar versão do *dotnet* instalada, pelo através do comando `dotnet info` no *prompt* de comando;
2) Listar versões do *sdk dotnet* instaladas através do comando `dotnet --list-sdks`;
3) No Visual Studio abrir a opção para criar novo projeto, selecionar os filtros '*All platforms*' e '*Web*';
4) Selecionar a opção *ASP.NET Core Web API*;
5) Informar dados do projeto (nome, path e solução);
6) Selecionar qual '*Target Framework*';
7) Criar projeto;

# Padrão MVC (Model-View-Controller)

O MVC (*Model-View-Controller*) é um padrão arquitetural que tem como objetivo separar a aplicação em três grupos de componentes, ajudando a alcançar separação de conceitos e responsabilidades. Com isso, a manutenção e testabilidade da aplicação ficam mais fáceis, visto que cada componente (`model, view ou controller`) tem um único trabalho.

Por exemplo, se código responsável pela apresentação de *user interface* e código com regras de negócio estão combinados em um único componente, a alteração deste é mais complexa e com maior risco a intrudução de erros. Na perspectiva do padrão MVC estes componentes estão separados, ou seja, possuem código separado.

![mvc diagram](https://docs.microsoft.com/en-us/aspnet/core/mvc/overview/_static/mvc.png?view=aspnetcore-6.0)

Usando este padrão, requisições são roteadas para um `Controller` que é responsável por trabalhar com o `Model` para realizar operações ou consultas. O `Controller`, então escolhe uma `View` para exibir os dados para o usuário através de um `Model`.

## Routing

Toda requisição HTTP de entrada é processada pelo componente de `rounting`, que despacha essas solicitações para `endpoints` configurados na aplicação. Um `endpoint` é uma unidade de código configurado quando a aplicação é iniciada para fazer o tratamento de uma requisição.

Podemos criar configurações manuais para o componente de `routing`, mas por padrão o `ASP.NET Core` entrega algumas configurações para roteamento, como: `Controllers (MVC Routing)`, `Razor Pages`, `SignalR`, `Health Checks` e outros.

### MVC Routing

Quando trabalhamos com um projeto Web API ou Web App (Razor Pages) utilizamos uma configuração de roteamento que faz um *match* das requisições de entrada para `Controllers` e `Actions` dentro do projeto, baseando em uma convenção de *template*.

Abaixo segue um exemplo da configuração tradicional em `Startup.cs`

```c#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRouting();
    
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```

Por padrão o componente de `routing` irá usar a seguinte convenção para fazer o match de uma URI: `{controler}/{action}/{id?}`. Ou seja, considerando a URI `Produtos/Detalhes/5` o componente de `rounting` irá enviar a requisição para um `controller` com nome de `Produtos`, executando um método (`action`) `Detalhes` que recebe um parametro `id`.

```c#
public class ProdutosController : Controller
{
    public IActionResult Detalhes(int id)
    {
        return ControllerContext.MyDisplayRouteInfo(id);
    }
}
```

## Model Binding

Dentro de uma requisição HTTP/HTTPS dados podem ser enviados de diferentes formas, como `form`, `query string`, `headers` e `parameters`. Cabe ao componente de `model binding` do `ASP.NET Core MVC` converter estes dados recebidos em objetos que um `controller` pode trabalhar. O resultado disto é que não precisamos introduzir código para fazer essas conversões e focar na lógica de negócio.

```c#
public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null) { ... }
```

### Model Validation

Além do motor de `binding` o `ASP.NET CORE MVC` oferece suporte a um componente de validação, que permite decorar nossos modelos (`model`) com atributos.

```c#
using System.ComponentModel.DataAnnotations;
public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}
```

# Web API

> Uma web API server-side é uma interface programática consistente de um ou mais endpoints publicamente expostos para um sistema definido de mensagens pedido-resposta, tipicamente expressado em JSON ou XML[2], que é exposto via a internet—mais comumente por meio de um servidor web baseado em HTTP.
[wikipédia](https://pt.wikipedia.org/wiki/Web_API#:~:text=Uma%20Web%20API%20%C3%A9%20uma,de%20dados%20de%20um%20site)

## Verbos HTTPS

O protocolo de comunicação para uma Web API é o HTTP, que define que um conjunto de métodos de requisição responsáveis por indicar a ação a ser executada para um dado recurso. Ou seja, todoa requisição HTTP é composta por um `HTTP Verb` que define uma ação (ler, submeter, alterar, deletar e etc) a ser realizada em um recurso.

Os verbos mais comuns são: 

| Verbo  |                                                                                                                                                                           |
|--------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| GET    | O método GET solicita a representação de um recurso específico. Requisições utilizando o método GET devem retornar apenas dados.                                          |
| POST   | O método POST é utilizado para submeter uma entidade a um recurso específico, frequentemente causando uma mudança no estado do recurso ou efeitos colaterais no servidor. |
| PUT    | O método PUT substitui todas as atuais representações do recurso de destino pela carga de dados da requisição.                                                            |
| DELETE | O método DELETE remove um recurso específico.                                                                                                                             |
| PATCH  | O método PATCH é utilizado para aplicar modificações parciais em um recurso.                                                                                              |

O `ASP.NET CORE` fornece alguns atributos como templates para auxiliar o roteamento de requisições para os `controllers`.

- `[HttpGet]`
- `[HttpPost]`
- `[HttPut]`
- `[HttpDelete]`
- `[HttpPatch]`

```c#

public class ProdutosController : Controller
{
    [HttpGet]
    public IActionResult Listar()
    {
        ...
    }

    [HttpPost]
    public IActionResult Criar()
    {
        ...
    }
}

```

## Respostas HTTP



## Controller Base

TODO

## Atributo ApiController 

TODO

## Binding source parameter

TODO

## Dependency Injection

TODO

# Exercícios

Criar um projeto ASP.NET Core WebApi para simular um aplicativo para registrar inscrições de alunos em turmas de atividades esportivas (futsal, volei, karate, etc) ofertadas por um complexo esportivo.

1) Criar um endpoint para cadastrar alunos, sendo dados obrigatórios:
- Nome
- Data de nascimento
- Endereço de email
2) Criar endpoint para listar alunos cadastrados;
3) Criar endpoint para cadastrar uma turma (descrição, quantidade de vagas, modalidade esportiva);
4) Criar endpoint para listar turmas;
5) Criar endpoint para realizar uma inscrição (matricular um aluno em uma turma existente);

Obs: os dados podem ser salvos em memória

