using FluentValidation;

namespace Webapi.Application.BookOperations.GetBookDetail{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public int bookId { get; set; }
        public GetBookDetailQueryValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
            RuleFor(query => query.BookId).NotEmpty().WithMessage("ID Boş Geçilmez!");
        }
    }
}