using FluentValidation;

namespace Webapi.Application.AuthorOperations.Commands
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator(){
            RuleFor(command => command.AuthorId).GreaterThan(0);
        }
    }
}