using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using N00019639.DB.Maps;
using N00019639.Models;

namespace N00019639.DB
{
    public class AppPokemonContext : DbContext
    {
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioPokemon> UsuarioPokemons { get; set; }

        public AppPokemonContext(DbContextOptions<AppPokemonContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PokemonMap());
            modelBuilder.ApplyConfiguration(new TipoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioPokemonMap());
        }
    }
}
