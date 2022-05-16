using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoApi.Domain.Commands;

namespace todoapi.domain.tests.CommandTests;

[TestClass]
public class CreateToDoCommandTests
{
    private readonly CreateToDoCommand _invalidCommand = new CreateToDoCommand("", DateTime.Now, "");
    private readonly CreateToDoCommand _validCommand = new CreateToDoCommand("Titulo", DateTime.Now, "Usuario");

    // nesse caso estamos validando dentro do construtor, porém temos que prestar a atenção 
    //pois caso seja criado um teste que não utilize o validate ele ia ser executado mesmo assim
    public CreateToDoCommandTests()
    {
        _invalidCommand.Validate();
        _validCommand.Validate();
    }

    [TestMethod]
    public void Dado_um_comando_invalido()
    {
        Assert.AreEqual(_invalidCommand.Valid, false);
    }

    [TestMethod]
    public void Dado_um_comando_valido()
    {
        Assert.AreEqual(_validCommand.Valid, true);
    }
}