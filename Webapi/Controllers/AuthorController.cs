using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Webapi.Application.AuthorOperations.Commands;
using Webapi.Application.AuthorOperations.Commands.CreateAuthors;
using Webapi.Application.AuthorOperations.Commands.UpdateAuthors;
using Webapi.Application.AuthorOperations.Queries.GetAuthor;
using Webapi.Application.AuthorOperations.Queries.GetAuthorDetail;
using Webapi.DBOperations;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper; 
        }

        [HttpGet]
        public IActionResult GetAuthor(){
            GetAuthorQuery query = new GetAuthorQuery(_dbContext,_mapper);
            var result = query.Handle();
            return Ok(result);
            
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorDetail(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_dbContext,_mapper);
            query.AuthorId = id;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);

            AuthorDetailViewModel result = query.Handle();
            
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_dbContext,_mapper);

            command.Model = newAuthor;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id,[FromBody] UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_dbContext);
            command.AuthorId = id;
            command.Model = updatedAuthor;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_dbContext);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            
            command.Handle();
            return Ok();
        }
    }
}