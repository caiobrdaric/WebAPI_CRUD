using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeRegistros.Models;

namespace SistemaDeRegistros.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Models.UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Property(x => x.CPF).IsRequired().HasMaxLength(30);
        }
    }
}
