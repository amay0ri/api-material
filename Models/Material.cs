namespace ApiMaterial.Models
{
    public class Material
    {
        
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Descricao { get; set; }
            public int ColecaoId { get; set; }
            public string UsuarioCriadorId { get; set; }
            public string HoraCriado { get; set; }
            public byte[]? FotoMaterial { get; set; }
            public bool Disponivel { get; set; } = true;
            public string DataInclusao { get; set; }
            public int Quantidade { get; set; }
            public string EstadoDoMaterial { get; set; }


        }

    
}
