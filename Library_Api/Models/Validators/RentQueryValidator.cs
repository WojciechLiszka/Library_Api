using FluentValidation;

namespace Library_Api.Models.Validators
{
    public class RentQueryValidator : AbstractValidator<RentQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };

        public RentQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });
        }
    }
}