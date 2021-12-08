using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

using MediatR;

using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.JsonObjClasses.Piston;

namespace RestWebservice_RemoteCompiling.Command
{
    public class CreateExerciseCommand : BaseCommand<int>
    {
        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        
    }
}