using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class UpdateExerciseHandler : BaseHandler<UpdateExerciseCommand, CustomResponse<bool>>
    {
        private readonly IExerciseRepository _exerciseRepository;

        public UpdateExerciseHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository)
            : base(userRepository)
        {
            _exerciseRepository = exerciseRepository;
        }


        public override async Task<CustomResponse<bool>> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            Exercise current = _exerciseRepository.Get(request.Id);

            current.Name = request.Name ?? current.Name;
            current.Description = request.Description ?? current.Description;
            current.TaskDefinition = request.Taskdefinition ?? current.TaskDefinition;
            
            List<ExerciseTemplateFiles> tempFileList = new List<ExerciseTemplateFiles>();
            foreach (FileEntity item in request.Template.Files)
            {
                ExerciseTemplateFiles y = new ExerciseTemplateFiles
                                          {
                                              FileName = item.FileName,
                                              LastModified = DateTime.Now
                                          };

                CheckPointEntity a = item.Checkpoints.Last();

                y.Checkpoints.Add(new Checkpoint
                                  {
                                      Code = a.Code,
                                      Created = DateTime.Now
                                  });

                tempFileList.Add(y);
            }

            current.Template = new ExerciseTemplateProject
                               {
                                   ProjectName = request.Template.ProjectName,
                                   Files = tempFileList,
                                   LastModified = DateTime.Now,
                                   ProjectType = request.Template.ProjectType,
                               };
            
            _exerciseRepository.Update(current);

            return CustomResponse.Success(true);
        }
    }
}