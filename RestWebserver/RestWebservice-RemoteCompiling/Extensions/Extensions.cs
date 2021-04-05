using System;
using Microsoft.AspNetCore.Mvc;
using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.JsonObjClasses;

namespace RestWebservice_RemoteCompiling.Extensions
{
    public static class Extensions
    {
        public static IActionResult ToResponse(this CustomResponse response)
        {
            if (!response.IsSuccess)
                return new ObjectResult(new { response.ErrorMessage }) { StatusCode = response.StatusCode };

            if (response.HasData)
                return new ObjectResult(new { Data = response.GetData() }) { StatusCode = response.StatusCode };

            return new StatusCodeResult(response.StatusCode);
        }
        
        public static SendCompileRequest ToJsonSendCompileRequest (this ExecuteCodeCommand command,IPistonHelper pistonHelper)
        {
            SendCompileRequest item = new SendCompileRequest
            {
                language = command.Language,
                version = command.Version,
                main = command.Code.mainFile,
                stdin = command.Code.stdin ,
                compile_timeout = Int32.Parse(pistonHelper.GetCompileTimeout()),
                run_timeout = Int32.Parse(pistonHelper.GetRunTimeout())
            };
            command.Code.files.ForEach(x => item.files.Add(x));
            command.Code.args.ForEach(x => item.args.Add(x));
            return item;
        }
    }
}