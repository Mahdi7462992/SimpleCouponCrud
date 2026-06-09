using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCouponCrud.Models.Entities;

namespace SimpleCouponCrud.Models.Configurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(p => p.DiscountType).HasConversion<string>().IsRequired();
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.Code).IsRequired();
            builder.Property(p => p.MinPurchaseAmount).IsRequired(false);
        }
    }
}
