
using FluentValidation;

namespace Webapi.BookOperations.GetBookDetail{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public int bookId { get; set; }
        public GetBookDetailQueryValidator()
        {
            RuleFor(command => command.BookId == bookId);
        }
    }
}