using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using N00019639.Models;

namespace N00019639.DB.Maps
{
    public class UsuarioPokemonMap : IEntityTypeConfiguration<UsuarioPokemon>
    {
        public void Configure(EntityTypeBuilder<UsuarioPokemon> builder)
        {
            builder.ToTable("UsuarioPokemon");
            builder.HasKey(o => new { o.PokemonId, o.UsuarioId });
            builder.HasOne(o => o.Usuario).WithMany(o => o.PokemonesCapturados).HasForeignKey(o => o.UsuarioId);
            builder.HasOne(o => o.Pokemon).WithMany(o => o.UsuarioPokemones).HasForeignKey(o => o.PokemonId);
        }
    }
}
