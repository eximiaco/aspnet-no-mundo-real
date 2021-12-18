namespace AplicacaoEscolas.WebApi.Dominio
{
    public sealed class EnderecoCompleto
  {
    public EnderecoCompleto(
      string rua,
      string numero,
      string complemento,
      string bairro,
      string cidade,
      string cep,
      string uF,
      string pais)
    {
      this.Rua = rua;
      this.Numero = numero;
      this.Complemento = string.IsNullOrWhiteSpace(complemento) ? string.Empty : complemento;
      this.Bairro = bairro;
      this.Cidade = cidade;
      this.Cep = cep;
      this.UF = uF;
      this.Pais = pais;
    }

    public string Rua { get; private set; }

    public string Numero { get; private set; }

    public string Complemento { get; private set; }

    public string Bairro { get; private set; }

    public string Cidade { get; private set; }

    public string Cep { get; private set; }

    public string UF { get; private set; }

    public string Pais { get; private set; }

    public static EnderecoCompleto CriarVazio() => 
      new EnderecoCompleto("", "", "", "", "", "", "", "");
  }
}