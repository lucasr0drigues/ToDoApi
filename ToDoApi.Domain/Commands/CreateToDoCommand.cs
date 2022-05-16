using Flunt.Notifications;
using Flunt.Validations;
using TodoApi.Domain.Commands.Contracts;

namespace TodoApi.Domain.Commands
{
    public class CreateToDoCommand : Notifiable, ICommand
    {
        public CreateToDoCommand() { }
        public CreateToDoCommand(string title, DateTime date, string user)
        {
            Title = title;
            Date = date;
            User = user;
        }

        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string User { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Title,3,"Title","por favor, descreva melhor esta tarefa")
                .HasMinLen(User,6,"User","Usuário inválido!")
            );
        }
    }
}