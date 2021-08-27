using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfinanceCadastro.Models
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public int idCidade { get; set; }
        public string nomeCliente { get; set; }
        public string cpfCnpjCliente { get; set; }
        public string telefoneCliente { get; set; }
        public Boolean statusCliente { get; set; }
    }
}