using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.JsonObjClasses;

namespace RestWebservice_RemoteCompiling.Validation
{
    public class ExecuteCodeValidator : CustomAbstractValidator<ExecuteCodeCommand>
    {

        private readonly IConfiguration _configuration;
        public ExecuteCodeValidator(IConfiguration configuration)
        {
            _configuration = configuration;
            
            RuleFor(x=>x)
                .Must(MaxFileSize)
                .WithMessage("max Payload 100kb");
            
            RuleFor(x => x.Language)
                .NotEmpty()
                .WithMessage("Language was empty");

            RuleFor(x => x.Version)
                .NotEmpty()
                .WithMessage("Version was empty");

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code Parameters were empty or wrong");

            RuleFor(x => x.Code.files)
                .NotEmpty()
                .WithMessage("No Files provided");

            RuleFor(x => x.Code.mainFile)
                .Must((o, mainFile) => IsValidMainFile(o.Code.files, mainFile))
                .WithMessage("mainFile not found");
            
            RuleFor(x => x.Code.stdin)
                .NotNull()
                .WithMessage("stdin is empty");
        }

        private bool MaxFileSize(ExecuteCodeCommand command)
        {
            string output = JsonConvert.SerializeObject(command);
            int maxSize = Convert.ToInt32(_configuration.GetSection("max_request_size").Value);
            if (output.Length > maxSize)
                return false;
            return true;
        }

        private bool IsValidMainFile(List<FileArray> array , string mainFile)
        {
            return array.FirstOrDefault(x => x.name == mainFile) != null;
        }
      
    }
}