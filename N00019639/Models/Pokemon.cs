using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public string Nombre { get; set; }
        public string RutaImagen { get; set; }
        public Tipo Tipo { get; set; }
        public List<UsuarioPokemon> UsuarioPokemones { get; set; }
    }
}
