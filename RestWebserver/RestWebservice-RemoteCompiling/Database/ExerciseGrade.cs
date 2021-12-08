using System.Collections.Generic;
using System.Linq;

namespace RestWebservice_RemoteCompiling.Database
{
    public class ExerciseGrade
    {
        public int Id { get; set; }
        public virtual Exercise Exercise { get; set; }
        public virtual User UserToGrade { get; set; }
        public int Grade { get; set; }
        public bool IsGraded { get; set; }
        public string Feedback { get; set; }
        
    }
}