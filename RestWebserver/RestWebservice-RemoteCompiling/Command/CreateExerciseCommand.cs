using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;

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

        public string TaskDefinition
        {
            get;
            set;
        }

        public ProjectType TemplateProjectType
        {
            get;
            set;
        }
        internal ExerciseTemplateProject template
        {
            get;
            set;
        } = new ExerciseTemplateProject();
    }
}