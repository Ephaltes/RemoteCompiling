using System;

namespace RestWebservice_RemoteCompiling.Database
{
    public class Checkpoint
    {
        public int Id
        {
            get;
            set;
        }

        public string Code
        {
            get;
            set;
        }

        public DateTime Created
        {
            get;
            set;
        } = DateTime.Now;
    }
}