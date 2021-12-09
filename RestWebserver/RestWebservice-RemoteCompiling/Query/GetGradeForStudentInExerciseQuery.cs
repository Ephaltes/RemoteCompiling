using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Query
{
    public class GetGradeForStudentInExerciseQuery : BaseCommand<ExerciseGrade>
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