using System.Collections.Generic;

using MediatR;

using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Query
{
    public class GetExercisesQuery : IRequest<CustomResponse<List<Entities.ExerciseEntity>>>
    {
        
    }
}