using System;
using FluentValidation;

namespace Webapi.Application.AuthorOperations.Commands.CreateAuthors{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command=> command.Model.Name).NotEmpty().MinimumLength(5);
            RuleFor(command => command.Model.BirthDay).NotEmpty().LessThan(DateTime.Now.Date);
            
            
        }
    }
}