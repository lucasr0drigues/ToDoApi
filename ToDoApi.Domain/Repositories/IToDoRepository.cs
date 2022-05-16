using ToDoApi.Domain.Entities;

namespace ToDoApi.Domain.Repositories
{
    public interface IToDoRepository
    {
        void Create(ToDoItem todo);
        void Update(ToDoItem todo);
        ToDoItem GetById(Guid id, string user);
        IEnumerable<ToDoItem> GetAll(string user);
        IEnumerable<ToDoItem> GetAllDone(string user);
        IEnumerable<ToDoItem> GetAllUndone(string user);
        IEnumerable<ToDoItem> GetByPeriod(string user, DateTime date, bool done);
    }
}