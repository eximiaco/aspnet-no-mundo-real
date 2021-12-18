namespace AplicacaoEscolas.WebApi.Hosting.Configuration
{
    public class MatriculasOptions
    {
        public const string Matriculas = "Matriculas";

        public bool Aberto { get; set; } = false;
        public string MensagemSucesso { get; set; } = "Matrícula realizada com sucesso";

    }
}
