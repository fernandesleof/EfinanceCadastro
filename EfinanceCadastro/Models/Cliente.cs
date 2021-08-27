using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfinanceCadastro.Models
{
    public class Cliente
    {
        private int idCliente { get; set; }
        private string nomeCliente { get; set; }
        private string cpfCnpjCliente { get; set; }
        private string telefoneCliente { get; set; }
        private Boolean statusCliente { get; set; }
    }
}