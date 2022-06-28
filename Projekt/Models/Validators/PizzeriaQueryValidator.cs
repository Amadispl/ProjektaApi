using FluentValidation;

namespace Projekt.Models.Validators
{
    public class PizzeriaQueryValidator:AbstractValidator<PizzeriaQuery>
    {
        private int[] allowedPageSizes = { 5, 10, 15 };
        public PizzeriaQueryValidator()
        {
           

            RuleFor(x => x.pageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.pageSize).Custom((value, context)=>
                {
                    if(!allowedPageSizes.Contains(value))
                    {
                        context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}] ");
                    }
            });

        }
    }
}
