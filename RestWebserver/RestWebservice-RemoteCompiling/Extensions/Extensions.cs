using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        public static SendCompileRequest ToJsonSendCompileRequest(this ExecuteCodeCommand command, IPistonHelper pistonHelper)
        {
            string version = command.Version;
            if (string.IsNullOrEmpty(version))
                version = pistonHelper.DefaultVersion(command.Language);

            if (string.IsNullOrEmpty(version))
                throw new VersionNotFoundException();


            SendCompileRequest item = new SendCompileRequest
                                      {
                                          language = command.Language,
                                          version = version,
                                          stdin = command.Code.stdin,
                                          compile_timeout = int.Parse(pistonHelper.GetCompileTimeout()),
                                          run_timeout = int.Parse(pistonHelper.GetRunTimeout())
                                      };

            command.Code.files.ForEach(x => item.files.Add(x));

            if (!string.IsNullOrWhiteSpace(command.Code.mainFile))
                MoveMainFileToFirstElement(item.files, command.Code.mainFile);

            command.Code.args.ForEach(x => item.args.Add(x));

            return item;
        }

        private static void MoveMainFileToFirstElement(List<FileArray> array, string mainFileName)
        {
            FileArray? mainFile = array.FirstOrDefault(x => x.name == mainFileName);

            if (mainFile == null)
                return;

            array.Remove(mainFile);
            array.Insert(0, mainFile);
        }
    }
}