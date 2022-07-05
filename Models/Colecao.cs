namespace ApiMaterial.Models
{
    public class Colecao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int CriadorId { get; set; }
    }
}
