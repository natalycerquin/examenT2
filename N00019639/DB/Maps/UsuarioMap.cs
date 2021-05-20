using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using N00019639.Models;

namespace N00019639.DB.Maps
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(o => o.Id);
            builder.HasMany(o => o.PokemonesCapturados).WithOne(o => o.Usuario).HasForeignKey(o => o.UsuarioId);
        }
    }
}
