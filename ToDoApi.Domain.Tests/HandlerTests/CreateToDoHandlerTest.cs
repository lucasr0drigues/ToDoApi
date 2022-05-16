using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoApi.Domain.Commands;
using ToDoApi.Domain.Commands;
using ToDoApi.Domain.Handlers;
using ToDoApi.Domain.Tests.Repositories;

namespace todoapi.domain.tests.HandlerTests;

[TestClass]
public class CreateToDoHandlerTest
{
    private readonly CreateToDoCommand _invalidCommand = new CreateToDoCommand("", DateTime.Now, "");
    private readonly CreateToDoCommand _validCommand = new CreateToDoCommand("Titulo", DateTime.Now, "Usuario");
    private readonly ToDoHandler _handler = new ToDoHandler(new FakeToDoRepository());
    private GenericCommandResult _result = new GenericCommandResult();

    [TestMethod]
    public void Dado_um_comando_invalido_deve_interromper_a_execucao()
    {
        _result = (GenericCommandResult)_handler.Handle(_invalidCommand);
        Assert.AreEqual(_result.Success, false);
    }

    [TestMethod]
    public void Dado_um_comando_valido_deve_criar_a_tarefa()
    {
        var _result = (GenericCommandResult)_handler.Handle(_validCommand);
        Assert.AreEqual(_result.Success, true);
    }
}