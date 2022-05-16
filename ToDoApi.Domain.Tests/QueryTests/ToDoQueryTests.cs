using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoApi.Domain.Commands;
using ToDoApi.Domain.Entities;
using ToDoApi.Domain.Queries;

namespace todoapi.domain.tests.QueryTests;

[TestClass]
public class ToDoQueryTests
{
    private List<ToDoItem> _items;

    public ToDoQueryTests()
    {
        _items = new List<ToDoItem>();
        _items.Add(new ToDoItem("tarefa teste1", DateTime.Now, "teste 1"));
        _items.Add(new ToDoItem("tarefa teste2", DateTime.Now, "teste 1"));
        _items.Add(new ToDoItem("tarefa teste3", DateTime.Now, "teste 2"));
        _items.Add(new ToDoItem("tarefa teste4", DateTime.Now, "teste 1"));
        _items.Add(new ToDoItem("tarefa teste5", DateTime.Now, "teste 2"));
    }

    [TestMethod]
    public void Dada_a_consulta_deve_retornar_tarefas_apenas_do_usuario_teste()
    {
        var result = _items.AsQueryable().Where(ToDoQueries.GetAll("teste 1")); // usando o .AsQueryable() para permitir passar uma função no .Where()
        Assert.AreEqual(3, result.Count());
    }
}