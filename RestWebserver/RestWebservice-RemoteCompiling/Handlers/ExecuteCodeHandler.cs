using System;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using RestWebservice_RemoteCompiling.JsonObjClasses.Piston;
using RestWebservice_RemoteCompiling.Repositories;

using Serilog;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class ExecuteCodeHandler : BaseHandler<ExecuteCodeCommand, CustomResponse<PistonCompileAndRun>>
    {
        private readonly IHttpHelper _httpHelper;
        private readonly IPistonHelper _pistonHelper;
        public ExecuteCodeHandler(IPistonHelper pistonHelper, IHttpHelper httpHelper, IUserRepository userRepository)
            : base(userRepository)
        {
            _pistonHelper = pistonHelper;
            _httpHelper = httpHelper;
        }

        public override async Task<CustomResponse<PistonCompileAndRun>> Handle(ExecuteCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                SendCompileRequest sendCompileRequest = request.ToJsonSendCompileRequest(_pistonHelper);

                var response = await _httpHelper.ExecutePost("api/v2/execute", sendCompileRequest);

                if (response.IsSuccessStatusCode)
                {
                    var settings = new JsonSerializerSettings
                                   {
                                       NullValueHandling = NullValueHandling.Ignore,
                                       MissingMemberHandling = MissingMemberHandling.Ignore,
                                   };

                    var resp = await response.Content.ReadAsStringAsync(cancellationToken);
                    var content = JsonConvert.DeserializeObject<PistonCompileAndRun>(resp, settings);

                    return CustomResponse.Success(content);
                }
                else
                {
                    var content = JsonConvert.DeserializeObject<PistonError>(await response.Content.ReadAsStringAsync(cancellationToken));

                    return CustomResponse.Error<PistonCompileAndRun>(500, content?.message);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, $"{e.Message} \n\n{e.StackTrace}");

                return CustomResponse.Error<PistonCompileAndRun>(500, "Unexpected Error");
            }
        }
    }
}