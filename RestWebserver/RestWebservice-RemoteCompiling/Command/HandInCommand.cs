﻿using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Command
{
    public class HandInCommand : BaseCommand<bool>
    {
        public int ProjectId
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