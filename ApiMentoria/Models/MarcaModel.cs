using System.ComponentModel;

namespace ApiMentoria.Models
{
    public class MarcaModel
    {
        public MarcaModel() { }

        public MarcaModel(string Nome, string cnpj, bool Status_M)
        {
            Nome = Nome;
            cnpj = cnpj;
            Status_M = Status_M;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string cnpj { get; set; }
        public bool Status_M { get; set; }
    }
}
