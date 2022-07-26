using FluentValidation;

namespace Webapi.Application.GenreOperations.UpdateGenre{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).When(x=>x.Model.Name != string.Empty);
        }
    }
}