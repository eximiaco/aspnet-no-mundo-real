# Carregando arquivos de configuração no `Asp.Net Core`

Configuração em aplicativos `Asp.Net Core` são realizados via um ou vários provedores de configuração. Onde um provedor de configuração é responsável em ler dados de configuração de um arquivo `key-value` utilizando uma variedade de fontes:

- arquivos de configuração, como por exemplo appsettings.json
- Environment variables
- Azure Key Vault
- Azure App Configuration
- Command-line arguments
- Provedores customizados
- Diretórios
- Objetos em memória

## Configuração padrão

Uma vez um aplicativo `Asp.Net Core` é criado via `cli` `dotnet new` ou via Visual Studio, temos o seguinte código dentro de `Program.cs` responsável em carregar a aplicação e por consequência uma configuração padrão.

``` csharp
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
```

O que acontece referente a configuração quando é inicializado a instância do `builder` é o seguinte:

1) Carregar `appsettings.json` pelo provedor `JSON configuration provider`
2) Carregar `appsettings.ENVIRONMENT.json` pelo provedor `JSON configuration provider`. Por exemplo, `appsettings.Production.json`
3) Carregar `App Secrets` para ambiente de `Development` via `Environment`
4) Variáveis informadas em `Environment` e carregadas pelo `Environment variables configuration provider`
5) Argumentos de linha de comando usando `Command-line configuration provider`

Importante observar que configurações adicionadas por último sobreescrevem antigos valores. Ou seja, se `minha-chave` for informada no `appsettings.json` e passado via linha de comando, seu valor será definido pela linha de comando.

## appsettings.json

Considere a seguinte configuração de um arquivo `appsettings.json`

``` json
{
  "Usuario": {
    "Funcao": "Avenger",
    "Nome": "Tonny Stark"
  },
  "MinhaChaveConfiguracao": "My appsettings.json Value",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

Para acessar os valores do arquivo, utilizamos a interface `IConfiguration`, como o exemplo abaixo:

``` csharp
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracoesExemploController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfiguracoesExemploController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Recuperar()
        {
            var minhaChave = _configuration["MinhaChaveConfiguracao"];
            var usuarioFuncao = _configuration["Usuario:Funcao"];
            var usuarioNome = _configuration["Usuario:Nome"];

            return Ok(new
            {
                minhaChave,
                usuarioNome,
                usuarioFuncao
            });
        }
    }
```

### Options Pattern

O padrão [options pattern](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0) utiliza classes para prover acesso tipado a configurações. Por exemplo:

``` json
  "Usuario": {
    "Funcao": "Avenger",
    "Nome": "Tonny Stark"
  }
```
Podemos criar uma classe `UsuarioOptions`:

```csharp
public class UsuarioOptions
{
    public const string Usuario = "Usuario";

    public string Funcao { get; set; } = String.Empty;
    public string Nome { get; set; } = String.Empty;
}
```

Para adicionar `UsuarioOptions` dentro do container de injeção de dependência usamos o seguinte código:

```csharp
 services.Configure<UsuarioOptions>(Configuration.GetSection(UsuarioOptions.Usuario));
```

Para consumir estes valores de configuração via a classe `UsuarioOptions` e injeção de dependência.

```csharp
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracoesExemploController : ControllerBase
    {
        private readonly UsuarioOptions _usuariosOptions;

        public ConfiguracoesExemploController(
            UsuarioOptions usuariosOptions)
        {
            _usuariosOptions = usuariosOptions;
        }

        [HttpGet()]
        public IActionResult Recuperar()
        {
            return Ok(_usuariosOptions);
        }
    }
```

## Segurança

Considere as seguintes orientações de segurança para trabalhar com configurações:

- Nunca armazene senhas ou outros dados sensíveis nos arquivos de configuração. Utilizar a ferramenta [Secret Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows#secret-manager) garante gaurdar estes dados sensíveis;
- Nunca use dados de produção em ambientes de teste ou desenvolvimento;
- Não utilize dados sensíveis em configurações que podem acidentalmente serem comitadas no repositório de código;