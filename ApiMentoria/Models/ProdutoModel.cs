using System.ComponentModel;

namespace ApiMentoria.Models
{
    public class ProdutoModel
    {
        public ProdutoModel() { }

        public ProdutoModel(string Name, float Valor, string Qualidade) 
        {
            Name = Name;
            Valor = Valor;
            Qualidade = Qualidade;
            Status = true;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Valor { get; set; }
        public string Qualidade { get; set; }
        public bool Status { get; set; }
        
    }
    
}

