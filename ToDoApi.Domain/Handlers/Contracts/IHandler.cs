using TodoApi.Domain.Commands.Contracts;
using ToDoApi.Domain.Commands;

namespace ToDoApi.Domain.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}