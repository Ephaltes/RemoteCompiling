using RestWebservice_RemoteCompiling.Database;

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

        internal ExerciseTemplateProject template
        {
            get;
            set;
        } = new ExerciseTemplateProject();
    }
}