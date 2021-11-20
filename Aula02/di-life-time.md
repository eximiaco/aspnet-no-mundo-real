# Ciclo de vida de Serviços

Serviços registrados no `container` de DI possuem um ciclo de vida para as instâncias de objetos criados. O ciclo de vida nada mais é que a duração desta instância durante a execução da aplicação.

O `ASP.NET Core` possui os seguintes ciclos de vida para registro de serviços:

- Transient
- Scoped
- Singleton

## Transient

Este ciclo de vida é o mais "curto", pois toda vez que solicitarmos para o `service container` um objeto registrado como `transient`, o mesmo irá criar uma nova instância.

Este tipo de registro é útil para classes leves e normalmente `stateless`, pois toda vez que solicitado teremos o custo de criação de um novo objeto e somente no final da requisição estes objetos serão liberados da memória (`disposed`).

Por exemplo, se um controller tem as dependências para os serviços A e B. Por sua vez o serviço B, tem como dependência o serviço A, ou seja, o serviço A é utilizando tanto dentro do controller como dentro do serviço B. Uma vez que o serviço A for registrado como `transient`, quando uma insntância do controller for requisita para o `service container` teremos **duas instâncias** do serviço A, a primeira para injetar no controller e a segunda para injetar no serviço B.

```c#
services.AddTransient<IServicoA, ServicoA>();
```

## Scoped

No caso de registro de serviços `scoped`, em aplicações web, teremos a criação de apenas uma instância de um objeto para toda a requisição do cliente e consequentemente este objeto será liberado (`disposed`) ao final da reuqisição.

Usando o exemplo anterior, se registramos o serviço A como `scoped`, quando o `service container` criar uma nova instância do `controller` para atender uma requisição, vamos ter apenas uma instância do serviço A criada e utilizada dentro do `controller` e dentro do serviço B. Somente ao final da requisição esta instância sofrerá o `diposed`.

Dado essa caracteristica deste ciclo de vida, seu uso é recomendado para casos como `Unit Of Work`. 

```c#
services.AddScoped<IServicoA, ServicoA>();
```

## Singleton

Uma instâcia de um objeto é criada pela primeira quando requisitada, e quando registrada como escopo `singleton`, qualquer outra solicitação para este serviço, mesmo por outra requisição web, receberá essa primeira instância criada. 

Como uma instância criada via `singleton` somente terá seu `disposed` quando a aplicação for desligada, consequentemente a memória não será liberada enquanto a aplicação estiver rodando. Assim, é importante observar o uso de memória para guardar estes objetos registrados como `singleton`.

```c#
services.AddSingleton<IServicoA, ServicoA>();
services.AddSingleton(new ServiceA());
```

## Transient vs Scoped vs Singleton

- Objetos criados via escopo `transient` serão sempre diferentes (novos).
- Objetos criados via escopo `scoped` serão os mesmos para um requisição completa, mas diferentes (novos) para cada nova requisição.
- Objetos criados via escopo `singleton` serão sempre os mesmos para todas as requisições.
  
## Dispose de objetos

 Considere os seguintes serviços:

```c#
public class Service1 : IDisposable
{
    private bool _disposed;

    public void Write(string message)
    {
        Console.WriteLine($"Service1: {message}");
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        Console.WriteLine("Service1.Dispose");
        _disposed = true;
    }
}

public class Service2 : IDisposable
{
    private bool _disposed;

    public void Write(string message)
    {
        Console.WriteLine($"Service2: {message}");
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        Console.WriteLine("Service2.Dispose");
        _disposed = true;
    }
}

public class Service3: IDisposable
{
    private bool _disposed;

    public void Write(string message)
    {
        Console.WriteLine($"Service3: {message}");
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        Console.WriteLine("Service3.Dispose");
        _disposed = true;
    }
}
``` 

Para configurar os serviços acima, utilizamos a seguinte configuração de `container`.

```c#
using DIsample2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<Service1>();
builder.Services.AddTransient<Service2>();
builder.Services.AddSingleton<Service3>();

var app = builder.Build();
```
Para consumir os serviços configurados, vamos usar o seguinte `controller` como exemplo:

```c#
[ApiController]
public class TesteController : ControllerBase
{
    private readonly Service1 _service1;
    private readonly Service2 _service2;
    private readonly IService3 _service3;

    public TesteController(Service1 service1, Service2 service2, IService3 service3)
    {
        _service1 = service1;
        _service2 = service2;
        _service3 = service3;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _service1.Write("OnGet");
        _service2.Write("OnGet");
        _service3.Write("OnGet");
        return Ok();
    }
}
```
Considerando o ciclo de vida de cada um dos serviços configurados e suas implementações, teriamos o seguinte resultado ao executar uma requisição para o `controller` em questão.

```console
Service1: IndexModel.OnGet
Service2: IndexModel.OnGet
Service3: IndexModel.OnGet
Service1.Dispose
Service2.Dispose
```

Ao final da requisição serviços criados sob o escopo `singleton` não tiveram a execução do `dispose`. Apenas serviços criados como `transient` e `scoped` tem a execução do `dispose` ao final da requisição.

### Serviços que não são criados pelo container

Considere os seguintes serviços configurados como `singleton`, mas tendo sua criação a critério do desenvolvedor, como ilustra o código abaixo:

```c#
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton(new Service1());
builder.Services.AddSingleton(new Service2());
```

O impacto deste tipo de configuração é:

- As instâncias do serviços não serão criadas pelo `container`.
- O framework não irá executar o método `dispose` destes serviços de forma automática.
- O desenvolvedor é resposnável por fazer o `dispose` deste serviços.

