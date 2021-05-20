using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using N00019639.Models;

namespace N00019639.DB.Maps
{
    public class PokemonMap : IEntityTypeConfiguration<Pokemon>
    {
        public void Configure(EntityTypeBuilder<Pokemon> builder)
        {
            builder.ToTable("Pokemon");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Tipo)
                .WithMany()
                .HasForeignKey(o => o.TipoId);
        }
    }
}
