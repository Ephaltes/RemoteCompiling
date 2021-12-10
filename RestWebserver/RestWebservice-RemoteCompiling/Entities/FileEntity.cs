using System;
using System.Collections.Generic;

namespace RestWebservice_RemoteCompiling.Entities
{
    public class FileEntity
    {
        public int Id
        {
            get;
            set;
        }

        public DateTime LastModified
        {
            get;
            set;
        } = DateTime.Now;

        public string FileName
        {
            get;
            set;
        }

        public virtual ICollection<CheckPointEntity> Checkpoints
        {
            get;
            set;
        } = new List<CheckPointEntity>();
    }
}