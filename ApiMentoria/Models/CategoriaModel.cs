namespace ApiMentoria.Models
{
    public class CategoriaModel
    {
        public CategoriaModel() { }

        public CategoriaModel(string Nome, bool Status_C)
        {
            Nome = Nome;
            Status_C = Status_C;

        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status_C { get; set; }
    }
}
