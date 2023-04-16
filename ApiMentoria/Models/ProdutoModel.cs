using System.ComponentModel;

namespace ApiMentoria.Models
{
    public class ProdutoModel
    {
        public ProdutoModel() { }

        public ProdutoModel(string Name, float Valor, string Quantidade) 
        {
           
            Name = Name;
            Valor = Valor;
            Quantidade = Quantidade;
            Status = true;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Valor { get; set; }
        public string Quantidade { get; set; }
        public bool STATUS_Produto { get; set; }
        
    }
    
}

