using FluentValidation;

namespace RestWebservice_RemoteCompiling.Validation
{
    public class CustomAbstractValidator<T>:AbstractValidator<T>
    {
        public CustomAbstractValidator()
        {
            CascadeMode = CascadeMode.Stop;
        }
    }
}