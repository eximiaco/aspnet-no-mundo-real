# HTTPS e APIs

Em uma aplicação `Asp.Net Core` podemos forçar o uso de `HTTPs` de duas formas:

- Midleware `UseHttpsRedirection()`
- Atributo de filtro `[RequireHttps]`

A diferença entre os dois métodos é que `UseHttpsRedirection()` retorna o status code 307, ou seja, informa que a chamada deve ser redirecionada para outro endereço. Já `[RequireHttps]` recusa a conexão caso tenha originado via HTTP.

No caso de desenvolvimento de web APIs, a microsoft recomenda o seguinte:

> **Do not use RequireHttpsAttribute** on Web APIs that receive sensitive information. RequireHttpsAttribute uses HTTP status codes to redirect browsers from HTTP to HTTPS. API clients may not understand or obey redirects from HTTP to HTTPS. Such clients may send information over HTTP.

A solução recomendada seria não iniciar um servidor escutando via HTTP ou em caso de aceitar conexões, fechar a mesma com status code 400.

Para a [segunda opção](https://recaffeinate.co/post/enforce-https-aspnetcore-api/), podemos utilizar um atributo de filtro que sobrescreve o comportamento de `[RequireHttps]`.

```csharp
    public class RequireHttpsOrCloseAttribute : RequireHttpsAttribute
    {
        protected override void HandleNonHttpsRequest(AuthorizationFilterContext filterContext)
        {
            filterContext.Result = new StatusCodeResult(400);
        }
    }
```

# Cross-Origin Resource Sharing (CORS)

Os *browsers* implementam um mecanismo de segurança que previne páginas de fazerem requisições para domínios diferntes daquele que está servindo a página. Essa restrição se chama *same-origin-pollicy*. Mas para alguns casos precisamos fazer com que os sites realizem requisições *cross-origin*, para isso utilizamos políticas de [CORS](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS).

## Mesma Origem

Duas URLs tem a mesma origin se elas possuírem o mesmo protocolo, host e porta.

Por exemplo, considere as seguintes URLs:

- `https://example.com/foo.html`
- `https://example.com/bar.html`

As URLs abaixo tem origens diferentes das informadas acima:

- `https://example.net` : Diferente domínio
- `https://www.example.com/foo.html` : Subdomínio diferente
- `http://example.com/foo.html` : Protocolo diferente
- `https://example.com:9000/foo.html` : Porta Diferente

## Habilitando CORS

Quando configuramos os serviços em um aplicação `Asp.Net Core` podemos adicionar [políticas CORS](https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0), como o exemplo abaixo:

```csharp
    services.AddCors(options =>
    {
        options.AddPolicy(name: "myAllowSpecificOrigins",
                        builder =>
                        {
                            builder.WithOrigins("http://example.com",
                                                "http://www.contoso.com");
                        });
    });
```

E na configuração de *middlewares* adicionar o controle destas políticas:

```csharp
    app.UseCors("myAllowSpecificOrigins");
```

## Política padrão

Podemos adicionar uma política padrão.

```csharp
    services.AddDefaultPolicy(options =>
    {
        builder =>
        {
            builder.WithOrigins("http://example.com",
                                "http://www.contoso.com");
        });
    });
```

E na configuração de *middlewares* adicionar o controle destas políticas:

```csharp
    app.UseCors();
```

