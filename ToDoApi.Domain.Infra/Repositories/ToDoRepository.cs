using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Infra.Contexts;
using ToDoApi.Domain.Entities;
using ToDoApi.Domain.Queries;
using ToDoApi.Domain.Repositories;

namespace ToDo.Domain.Infra.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly DataContext _context;

        public ToDoRepository(DataContext context)
        {
            _context = context;
        }

        public ToDoRepository()
        {

        }

        public void Create(ToDoItem todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();
        }

        public IEnumerable<ToDoItem> GetAll(string user)
        {
            return _context.Todos.AsNoTracking().Where(ToDoQueries.GetAll(user)).OrderBy(x => x.Date);
        }

        public IEnumerable<ToDoItem> GetAllDone(string user)
        {
            return _context.Todos.AsNoTracking().Where(ToDoQueries.GetAllDone(user)).OrderBy(x => x.Date);
        }

        public IEnumerable<ToDoItem> GetAllUndone(string user)
        {
            return _context.Todos.AsNoTracking().Where(ToDoQueries.GetAllUndone(user)).OrderBy(x => x.Date);
        }

        public ToDoItem GetById(Guid id, string user)
        {
            return _context.Todos.AsNoTracking().FirstOrDefault(x => x.Id == id && x.User == user);
        }

        public IEnumerable<ToDoItem> GetByPeriod(string user, DateTime date, bool done)
        {
            return _context.Todos.AsNoTracking().Where(ToDoQueries.GetByPeriod(user, date, done)).OrderBy(x => x.Date);
        }

        public void Update(ToDoItem todo)
        {
            _context.Entry(todo).State = EntityState.Modified; // informando ao EF que o campo foi modificado, o EF irá verificar cada campo do todo para realizar o update
            _context.SaveChanges();
        }
    }
}