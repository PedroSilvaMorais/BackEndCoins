using Coins.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coins.Data.Mappings
{
    public class MoedaMapping : IEntityTypeConfiguration<Coin>
    {
        public void Configure(EntityTypeBuilder<Coin> builder)
        {
            builder.ToTable("Moedas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Moeda)
                .IsRequired()
                .HasColumnType("varchar(50)");
        }
    }
}
