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
                .WithMessage("Language was empty");
            RuleFor(x => x.Version).NotEmpty()
                .WithMessage("Version was empty");
            RuleFor(x => x).Must((x) => FileExists(x.Language, x.Version)).WithMessage($"No Template for Language and Version found");
        }

        private bool FileExists(string language, string version)
        {
            if (File.Exists($"./Templates/{language.ToLower()}Template_{version}.json"))  //TODO: possibly wrong path in production
                return true;

            return false;
        }
        
    }
}