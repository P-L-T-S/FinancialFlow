using FinancialCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialFlow.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");
        builder.HasKey(category => category.Id);

        builder.Property(category => category.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(category => category.Description)
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);

        builder.Property(category => category.UserId)
            .IsRequired();
    }
}