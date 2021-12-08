using System.Collections.Generic;

using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Command
{
    public class HandInCommand : BaseCommand<bool>
    {
        public List<string> FilesNames
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