using TodoApi.Domain.Commands;
using ToDoApi.Domain.Commands;
using ToDoApi.Domain.Entities;
using ToDoApi.Domain.Handlers.Contracts;
using ToDoApi.Domain.Repositories;

namespace ToDoApi.Domain.Handlers
{
    public class ToDoHandler :
    Flunt.Notifications.Notifiable,
    IHandler<CreateToDoCommand>,
    IHandler<UpdateToDoCommand>,
    IHandler<MarkToDoAsDoneCommand>,
    IHandler<MarkToDoAsUndoneCommand>
    {
        private readonly IToDoRepository _repository;

        public ToDoHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateToDoCommand command)
        {
            // fail fast validation
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
            }

            // gerar o todo item
            var todo = new ToDoItem(command.Title, command.Date, command.User);

            // salvar um todo no banco
            _repository.Create(todo);

            // notificar o usuario
            return new GenericCommandResult(true, "Tarefa salva", todo);

        }

        public ICommandResult Handle(UpdateToDoCommand command)
        {
            // fail fast validation
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
            }

            // recupera o todo item (rehidratação)
            var todo = _repository.GetById(command.Id, command.User);

            // altera o titulo
            todo.UpdateTitle(command.Title);

            // salvar um todo no banco
            _repository.Update(todo);

            // notificar o usuario
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handle(MarkToDoAsDoneCommand command)
        {
            // fail fast validation
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
            }

            // recupera o todo item (rehidratação)
            var todo = _repository.GetById(command.Id, command.User);

            // altera o estado
            todo.MarkAsDone();

            // salvar um todo no banco
            _repository.Update(todo);

            // notificar o usuario
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handle(MarkToDoAsUndoneCommand command)
        {
            // fail fast validation
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
            }

            // recupera o todo item (rehidratação)
            var todo = _repository.GetById(command.Id, command.User);

            // altera o estado
            todo.MarkAsUndone();

            // salvar um todo no banco
            _repository.Update(todo);

            // notificar o usuario
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }
    }
}