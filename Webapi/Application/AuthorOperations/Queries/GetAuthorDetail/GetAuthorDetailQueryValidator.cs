using FluentValidation;

namespace Webapi.Application.AuthorOperations.Queries.GetAuthorDetail{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public int authorId { get; set; }

        public GetAuthorDetailQueryValidator()
        {
            RuleFor(query => query.AuthorId).NotEmpty().GreaterThan(0);
        }
    }
}