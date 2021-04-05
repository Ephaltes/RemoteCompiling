using System.IO;
using FluentValidation;
using RestWebservice_RemoteCompiling.Query;

namespace RestWebservice_RemoteCompiling.Validation
{
    public class GetTemplateForLanguageValidator : CustomAbstractValidator<GetTemplateForLanguageQuery>
    {

        public GetTemplateForLanguageValidator()
        {
            RuleFor(x => x.Language)
                .NotEmpty()
                .WithMessage("Language was empty")
                .Must(FileExists)
                .WithMessage("No Template for Language");
        }

        private bool FileExists(string language)
        {
            if (File.Exists($"./Templates/{language.ToLower()}Template.json"))  //TODO: possibly wrong path in production
                return true;

            return false;
        }
        
    }
}