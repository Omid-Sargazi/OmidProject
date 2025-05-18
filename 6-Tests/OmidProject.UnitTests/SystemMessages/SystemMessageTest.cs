using System;
using System.Collections.Generic;
using OmidProject.Domains.Domain.SystemMessages;
using OmidProject.Domains.Domain.SystemMessages.Exceptions;
using OmidProject.Frameworks.Contracts.Common.Enums;
using FluentAssertions;
using Xunit;

namespace OmidProject.UnitTests.SystemMessages;

public class SystemMessageTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_Exception_When_Id_Is_Invalid(int value)
    {
        Action action = () => new SystemMessage(value, TypeSystemMessage.Error, new List<SystemDataMessage>());

        action.Should().Throw<SystemErrorCodeIsInvalidException>();
    }

    [Fact]
    public void Should_Throw_Exception_When_SystemDataMessage_Is_Null()
    {
        Action action = () => new SystemMessage(1, TypeSystemMessage.Error, new List<SystemDataMessage>());

        action.Should().Throw<SystemErrorMessageIsEmptyException>();
    }

    [Fact]
    public void Should_Not_Throw_Exception_When_Values_Are_Valid()
    {
        var message = new List<SystemDataMessage>
        {
            new(ContentLanguage.English, "test", "test"),
            new(ContentLanguage.Persian, "test", "test")
        };

        Action action = () => new SystemMessage(1, TypeSystemMessage.Error, message);

        action.Should().NotThrow();
    }
}