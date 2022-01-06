using System.IdentityModel.Tokens.Jwt;

using MediatR;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Query
{
    public class GetGradingStatusQuery : IRequest<CustomResponse<GradingStatus>>
    {
        internal JwtSecurityToken Token
        {
            get;
            set;
        }
        public string StudentId
        {
            get;
            set;
        }

        public int ExerciseId
        {
            get;
            set;
        }
    }
}