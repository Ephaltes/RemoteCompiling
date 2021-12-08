﻿using System.Collections.Generic;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Query
{
    public class GetExerciseQuery : IRequest<CustomResponse<Exercise>>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}