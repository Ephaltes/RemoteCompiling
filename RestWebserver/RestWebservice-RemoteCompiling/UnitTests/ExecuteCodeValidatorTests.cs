using System;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using RestWebservice_RemoteCompiling.Validation;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ExecuteCodeValidator_OK()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigurationSection = new Mock<IConfigurationSection>();
            var validator = new ExecuteCodeValidator(mockConfiguration.Object);
            var executeCommand = new ExecuteCodeCommand();
            var file = new FileArray();

            mockConfigurationSection.Setup(x => x.Value).Returns("100000");
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(mockConfigurationSection.Object);

            file.content = String.Empty;
            file.name = "test.cs";
            
            executeCommand.Language = "csharp";
            executeCommand.Version = "5.0.201";
            executeCommand.Code = new Code();
            executeCommand.Code.mainFile = "test.cs";
            executeCommand.Code.files.Add(file);
            
            Assert.That(validator.Validate(executeCommand).IsValid);
        }
        
        [Test]
        public void ExecuteCodeValidator_OK_noMainFile()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigurationSection = new Mock<IConfigurationSection>();
            var validator = new ExecuteCodeValidator(mockConfiguration.Object);
            var executeCommand = new ExecuteCodeCommand();
            var file = new FileArray();

            mockConfigurationSection.Setup(x => x.Value).Returns("100000");
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(mockConfigurationSection.Object);

            file.content = String.Empty;
            file.name = "test.cs";
            
            executeCommand.Language = "csharp";
            executeCommand.Version = "5.0.201";
            executeCommand.Code = new Code();
            executeCommand.Code.files.Add(file);
            
            Assert.That(validator.Validate(executeCommand).IsValid);
        }
        
        [Test]
        public void ExecuteCodeValidator_fails_maxFileSize()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigurationSection = new Mock<IConfigurationSection>();
            var validator = new ExecuteCodeValidator(mockConfiguration.Object);
            var executeCommand = new ExecuteCodeCommand();
            var file = new FileArray();

            mockConfigurationSection.Setup(x => x.Value).Returns("100000");
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(mockConfigurationSection.Object);

            file.content = new string('*', 200000);
            file.name = "test.cs";
            
            executeCommand.Language = "csharp";
            executeCommand.Version = "5.0.201";
            executeCommand.Code = new Code();
            executeCommand.Code.mainFile = "test.cs";
            executeCommand.Code.files.Add(file);

            var errors = validator.Validate(executeCommand);
            Assert.That(errors.Errors.Count == 1);
        }
        
        [Test]
        public void ExecuteCodeValidator_fails_languageEmpty()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigurationSection = new Mock<IConfigurationSection>();
            var validator = new ExecuteCodeValidator(mockConfiguration.Object);
            var executeCommand = new ExecuteCodeCommand();

            mockConfigurationSection.Setup(x => x.Value).Returns("100000");
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(mockConfigurationSection.Object);

            executeCommand.Language = "";
            executeCommand.Version = "5.0.201";

            var errors = validator.Validate(executeCommand);
            Assert.That(errors.Errors.Count == 1);
        }
        
        [Test]
        public void ExecuteCodeValidator_fails_versionEmpty()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigurationSection = new Mock<IConfigurationSection>();
            var validator = new ExecuteCodeValidator(mockConfiguration.Object);
            var executeCommand = new ExecuteCodeCommand();

            mockConfigurationSection.Setup(x => x.Value).Returns("100000");
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(mockConfigurationSection.Object);

            executeCommand.Language = "csharp";
            executeCommand.Version = "";

            var errors = validator.Validate(executeCommand);
            Assert.That(errors.Errors.Count == 1);
        } 
        
        [Test]
        public void ExecuteCodeValidator_fails_codeNull()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigurationSection = new Mock<IConfigurationSection>();
            var validator = new ExecuteCodeValidator(mockConfiguration.Object);
            var executeCommand = new ExecuteCodeCommand();

            mockConfigurationSection.Setup(x => x.Value).Returns("100000");
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(mockConfigurationSection.Object);

            executeCommand.Language = "csharp";
            executeCommand.Version = "5.0.201";
            executeCommand.Code = null;
            
            var errors = validator.Validate(executeCommand);
            Assert.That(errors.Errors.Count == 1);
        }
        
        [Test]
        public void ExecuteCodeValidator_fails_codeFileEmpty()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigurationSection = new Mock<IConfigurationSection>();
            var validator = new ExecuteCodeValidator(mockConfiguration.Object);
            var executeCommand = new ExecuteCodeCommand();

            mockConfigurationSection.Setup(x => x.Value).Returns("100000");
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(mockConfigurationSection.Object);

            executeCommand.Language = "csharp";
            executeCommand.Version = "5.0.201";
            executeCommand.Code = new Code();

            var errors = validator.Validate(executeCommand);
            Assert.That(errors.Errors.Count == 1);
        }
        

        
        [Test]
        public void ExecuteCodeValidator_fails_mainFileNotFound()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigurationSection = new Mock<IConfigurationSection>();
            var validator = new ExecuteCodeValidator(mockConfiguration.Object);
            var executeCommand = new ExecuteCodeCommand();
            var file = new FileArray();

            mockConfigurationSection.Setup(x => x.Value).Returns("100000");
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(mockConfigurationSection.Object);

            file.content = String.Empty;
            file.name = "test.cs";
            
            executeCommand.Language = "csharp";
            executeCommand.Version = "5.0.201";
            executeCommand.Code = new Code();
            executeCommand.Code.mainFile = "test32.cs";
            executeCommand.Code.files.Add(file);
            
            var errors = validator.Validate(executeCommand);
            Assert.That(errors.Errors.Count == 1);
        }
    }
}