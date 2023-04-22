using System.ComponentModel;

namespace ApiMentoria.Models
{
    public class ProdutoModel
    {
        public ProdutoModel() { }

        public ProdutoModel(string Name, float Valor, int Quantidade, bool Status_P) 
        {
           
            Name = Name;
            Valor = Valor;
            Quantidade = Quantidade;
            Status_P = Status_P;

         
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Valor { get; set; }
        public int Quantidade { get; set; }
        public bool Status_P { get; set; }
        public int IdMarca { get; set; }
        public int IdCategoria { get; set; }

    }
    
}

