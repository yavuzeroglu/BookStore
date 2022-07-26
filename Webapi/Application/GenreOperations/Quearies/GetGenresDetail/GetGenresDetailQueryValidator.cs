using FluentValidation;

namespace Webapi.Application.GenreOperations.Queries.GetGenresDetail
{
    public class GetGenresDetailQueryValidator : AbstractValidator<GetGenresDetailQuery>
    {
        public GetGenresDetailQueryValidator(){
            RuleFor(query=>query.GenreId).GreaterThan(0);
        }    
    }   
}