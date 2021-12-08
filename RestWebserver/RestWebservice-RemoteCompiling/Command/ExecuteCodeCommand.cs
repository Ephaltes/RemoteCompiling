using System.ComponentModel.DataAnnotations;
using MediatR;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using RestWebservice_RemoteCompiling.JsonObjClasses.Piston;

namespace RestWebservice_RemoteCompiling.Command
{
    public class ExecuteCodeCommand : IRequest<CustomResponse<PistonCompileAndRun>>
    {
        public string Language { get; set; }
        public string Version { get; set; }
        public JsonObjClasses.Code Code { get; set; }
    }
}