using System.Collections.Generic;

using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Command
{
    public class UpdateExerciseCommand : BaseCommand<bool>
    {
        public int Id
        {
            get;
            set;
        }
        public string? Name
        {
            get;
            set;
        }

        public string? Description
        {
            get;
            set;
        }

        public List<Files> Files
        {
            get;
            set;
        } = new List<Files>();
    }
}