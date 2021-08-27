using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfinanceCadastro.Models
{
    public class Cidade
    {
        public int idCidade { get; set; }
        public int codigoIbgeCidade { get; set; }
        public string nomeCidade { get; set; }
        public int idEstado { get; set; }
    }
}