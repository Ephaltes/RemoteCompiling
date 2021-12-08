using System;
using System.Collections.Generic;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Entities
{
    public class Excercise
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        
        public string Author
        {
            get;
            set;
        }
        
        public string Description
        {
            get;
            set;
        }
        
        public string TaskDefinition
        {
            get;
            set;
        }
        
        public DateTime DueDate
        {
            get;
            set;
        }
        
        public List<Files> Files { get; set; } 
        
        public List<Students> Students { get; set; }
    }
}