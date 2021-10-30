using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWebservice_RemoteCompiling.Database
{
    public class File
    {
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public string FileName { get; set; }
        public Collection<Checkpoint> Checkpoints { get; set; } = new();
    }
}