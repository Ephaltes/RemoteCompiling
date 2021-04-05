using System.Data;
using System.IO;
using FluentValidation;
using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Query;

namespace RestWebservice_RemoteCompiling.Validation
{
    public class ExecuteCodeValidator : CustomAbstractValidator<ExecuteCodeCommand>
    {

        public ExecuteCodeValidator()
        {
            RuleFor(x => x.Language)
                .NotEmpty()
                .WithMessage("Language was empty");

            RuleFor(x => x.Version)
                .NotEmpty()
                .WithMessage("Version was empty");

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code Parameters were empty or wrong");

            RuleFor(x => x.Code.mainFile)
                .NotNull()
                .WithMessage("MainFile is empty");
            
            RuleFor(x => x.Code.stdin)
                .NotNull()
                .WithMessage("stdin is empty");
        }
      
    }
}