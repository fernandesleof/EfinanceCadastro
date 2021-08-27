using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfinanceCadastro.Models
{
    public class Estado
    {
        public int idEstado { get; set; }
        public int codUfEstado { get; set; }
        public string nomeEstado { get; set; }
        public string ufEstado { get; set; }
    }
}