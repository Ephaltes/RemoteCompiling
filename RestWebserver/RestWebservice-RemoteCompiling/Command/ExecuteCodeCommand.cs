using System.ComponentModel.DataAnnotations;
using MediatR;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.JsonObjClasses;

namespace RestWebservice_RemoteCompiling.Command
{
    public class ExecuteCodeCommand : IRequest<CustomResponse<PistonCompileAndRun>>
    {
        public string Language { get; }
        public string Version { get; }
        public Code Code { get; }

        public ExecuteCodeCommand(string language, string version, Code code)
        {
            Language = language;
            Version = version;
            Code = code;
        }
    }
}