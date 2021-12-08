using System.Collections.Generic;

using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Command
{
    public class UpdateExerciseCommand : BaseCommand<bool>
    {
        internal int Id
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

        public ProjectEntity TemplateProject
        {
            get;
            set;
        }
    }
}