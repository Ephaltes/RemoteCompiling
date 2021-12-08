using System.Collections.Generic;

namespace RestWebservice_RemoteCompiling.Entities
{
    public class Students
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

        public List<Files> Files
        {
            get;
            set;
        }
    }
}