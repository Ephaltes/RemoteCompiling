using System.Collections.Generic;

namespace RestWebservice_RemoteCompiling.Entities
{
    public class StudentEntity
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
            
        }

        public int Grade
        {
            get;
            set;
        }

        public bool IsGraded
        {
            get;
            set;
        }

        public string Feedback
        {
            get;
            set;
        }

        public List<ProjectEntity> Projects
        {
            get;
            set;
        } = new List<ProjectEntity>();
    }
}