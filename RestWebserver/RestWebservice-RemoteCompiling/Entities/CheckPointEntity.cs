using System;

namespace RestWebservice_RemoteCompiling.Entities
{
    public class CheckPointEntity
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