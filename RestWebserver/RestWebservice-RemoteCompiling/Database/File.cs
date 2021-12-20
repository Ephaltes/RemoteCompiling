﻿using System;
using System.Collections.Generic;

namespace RestWebservice_RemoteCompiling.Database
{
    public class File
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

        public virtual ICollection<Checkpoint> Checkpoints
        {
            get;
            set;
        } = new List<Checkpoint>();


        public virtual Exercise Exercise
        {
            get;
            set;
        }
    }
}