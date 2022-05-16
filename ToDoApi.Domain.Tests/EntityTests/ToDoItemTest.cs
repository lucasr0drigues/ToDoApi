using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoApi.Domain.Commands;
using ToDoApi.Domain.Entities;

namespace todoapi.domain.tests.EntityTest;

[TestClass]
public class ToDoItemTest
{
    private readonly ToDoItem _validTodo = new ToDoItem("Titulo aqui", DateTime.Now, "test user");

    [TestMethod]
    public void Dado_um_novo_todo_o_mesmo_nao_pode_ser_concluido()
    {

        Assert.AreEqual(_validTodo.Done, false);
    }
}