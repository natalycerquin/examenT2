using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.Models
{
    public class UsuarioPokemon
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public DateTime Fecha { get; set; }
    }
}
