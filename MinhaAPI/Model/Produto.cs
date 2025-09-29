namespace MinhaAPI.Model
{
    public class Produto
    {
        public int Id { get; set; }                    // Identificador
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }             // Ex.: 199.90
    }
}
