using FluentValidation;
using SimpleCouponCrud.Models.BaseEntities;

namespace SimpleCouponCrud.Models.Entities
{
    public class Coupon: BaseEntity<Guid>, IDbSetEntity
    {
        public string Code { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Value { get; set; }
        public decimal? MinPurchaseAmount { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }
    }


    public enum DiscountType
    {
        Percentage,
        FixedAmount
    }

    public class CouponValidator : AbstractValidator<Coupon>
    {
        public CouponValidator()
        {
            When(x => x.DiscountType == DiscountType.Percentage, () =>
            {
                RuleFor(x => x.Value).InclusiveBetween(1, 100);
            });

            When(x => x.DiscountType == DiscountType.FixedAmount, () =>
            {
                RuleFor(x => x.Value).GreaterThan(0);
            });
        }
    }
}
