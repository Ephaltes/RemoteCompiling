using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Query
{
    public class GetExerciseHandInQuery : BaseCommand<ExerciseEntity>
    {
        [FromRoute]
        public int Id
        {
            get;
            set;
        }
    }
}