using FG.Domain;
using FG.Domain.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace FG.Tests.Services
{
    public class MenuTest
    {
        [Fact]
        public void Menu_Constructor_Test()
        {
            //Arrange

            //Act
            var menu = new Menu();

            //Assert
            menu.InitialQuestion.Should().NotBeNull();
        }
    }
}
