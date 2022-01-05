using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Query
{
    public class GetGradeForStudentInExerciseQuery : BaseCommand<ExerciseGradeEntity>
    {
        public string StudentId
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