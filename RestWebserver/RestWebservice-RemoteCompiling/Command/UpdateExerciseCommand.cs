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

        public ProjectEntity TemplateProject
        {
            get;
            set;
        }
    }
}