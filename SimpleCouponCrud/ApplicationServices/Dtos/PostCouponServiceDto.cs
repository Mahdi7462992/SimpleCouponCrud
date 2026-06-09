using FluentValidation;
using SimpleCouponCrud.Models.Entities;

namespace SimpleCouponCrud.ApplicationServices.Dtos
{
    public class PostCouponServiceDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Value { get; set; }
        public decimal? MinPurchaseAmount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class PostCouponServiceDtoValidator : AbstractValidator<PostCouponServiceDto>
    {
        public PostCouponServiceDtoValidator()
        {
            RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("وارد کردن کد تخفیف الزامی است.");

            RuleFor(x => x.ExpirationDate)
            .GreaterThan(DateTime.Now)
            .WithMessage("تاریخ انقضای کوپن باید بزرگتر از زمان فعلی باشد.");

            When(x => x.DiscountType == DiscountType.Percentage, () =>
            {
                RuleFor(x => x.Value)
                    .InclusiveBetween(1, 100)
                    .WithMessage("درصد تخفیف باید بین 1 تا 100 باشد.");
            });

            When(x => x.DiscountType == DiscountType.FixedAmount, () =>
            {
                RuleFor(x => x.Value)
                    .GreaterThan(0)
                    .WithMessage("مبلغ تخفیف باید بزرگتر از صفر باشد.");
            });
        }
    }
}
