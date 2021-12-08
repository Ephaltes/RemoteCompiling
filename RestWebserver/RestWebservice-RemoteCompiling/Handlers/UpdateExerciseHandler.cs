using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class UpdateExerciseHandler : IRequestHandler<UpdateExerciseCommand, CustomResponse<bool>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;

        public UpdateExerciseHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository)
        {
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
        }


        public async Task<CustomResponse<bool>> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            var current = _exerciseRepository.Get(request.Id);
            
            current.Name = request.Name ?? current.Name;
            current.Description = request.Description ?? current.Description;
            if (request.Files.Count > 0)
            {
                current.Files.Clear();
                foreach (var item in request.Files)
                {
                    var x = new ExerciseFile()
                            {
                                FileName = item.Name,
                                FileNodeType = item.Type,
                                Checkpoint = new Checkpoint()
                                             {
                                                 Code = item.Code.Value,
                                                 Created = DateTime.Now,
                                                 Stdin = ""
                                             },
                                LastModified = DateTime.Now,
                                User = null,
                                Exercise = current
                            };
                    current.Files.Add(x);
                }
            }
            _exerciseRepository.Update(current);
            return CustomResponse.Success(true);
        }
    }
}