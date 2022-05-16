using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Domain.Commands;
using ToDoApi.Domain.Commands;
using ToDoApi.Domain.Entities;
using ToDoApi.Domain.Handlers;
using ToDoApi.Domain.Repositories;

namespace todoapi.domain.api.Controllers;

[ApiController]
[Route("v1/todos")]
[Authorize]
public class ToDoController : ControllerBase
{
    [Route("")]
    [HttpGet]
    public IEnumerable<ToDoItem> GetAll([FromServices] IToDoRepository repository)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return repository.GetAll(user);
    }

    [Route("done")]
    [HttpGet]
    public IEnumerable<ToDoItem> GetAllDone([FromServices] IToDoRepository repository)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return repository.GetAllDone(user);
    }

    [Route("undone")]
    [HttpGet]
    public IEnumerable<ToDoItem> GetAllUndone([FromServices] IToDoRepository repository)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return repository.GetAllUndone(user);
    }

    [Route("done/today")]
    [HttpGet]
    public IEnumerable<ToDoItem> GetDoneForToday([FromServices] IToDoRepository repository)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return repository.GetByPeriod(user, DateTime.Now.Date, true);
    }

    [Route("undone/today")]
    [HttpGet]
    public IEnumerable<ToDoItem> GetUndoneForToday([FromServices] IToDoRepository repository)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return repository.GetByPeriod(user, DateTime.Now.Date, false);
    }

    [Route("done/tomorrow")]
    [HttpGet]
    public IEnumerable<ToDoItem> GetDoneForTomorrow([FromServices] IToDoRepository repository)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return repository.GetByPeriod(user, DateTime.Now.Date.AddDays(1), true);
    }

    [Route("undone/tomorrow")]
    [HttpGet]
    public IEnumerable<ToDoItem> GetUndoneForTomorrow([FromServices] IToDoRepository repository)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return repository.GetByPeriod(user, DateTime.Now.Date.AddDays(1), false);
    }

    [Route("")]
    [HttpPost]
    public GenericCommandResult Create([FromBody] CreateToDoCommand command, [FromServices] ToDoHandler handler)
    {
        command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return (GenericCommandResult)handler.Handle(command);
    }

    [Route("")]
    [HttpPut]
    public GenericCommandResult Update([FromBody] UpdateToDoCommand command, [FromServices] ToDoHandler handler)
    {
        command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return (GenericCommandResult)handler.Handle(command);
    }

    [Route("mark-as-done")]
    [HttpPut]
    public GenericCommandResult MarkAsDone([FromBody] MarkToDoAsDoneCommand command, [FromServices] ToDoHandler handler)
    {
        command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return (GenericCommandResult)handler.Handle(command);
    }

    [Route("mark-as-undone")]
    [HttpPut]
    public GenericCommandResult MarkAsUndone([FromBody] MarkToDoAsUndoneCommand command, [FromServices] ToDoHandler handler)
    {
        command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return (GenericCommandResult)handler.Handle(command);
    }
}
