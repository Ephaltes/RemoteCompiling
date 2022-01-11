using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class AddProjectHandler : BaseHandler<AddProjectCommand, CustomResponse<int>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        public AddProjectHandler(IUserRepository userRepository, IExerciseRepository exerciseRepository, IProjectRepository projectRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
            _projectRepository = projectRepository;
        }

        public override async Task<CustomResponse<int>> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = await _userRepository.GetUserByLdapUid(ldapIdent);

            if (ldapUser is null)
                return CustomResponse.Error<int>(403);

            Project project = new Project
                              {
                                  ProjectName = request.Project.ProjectName,
                                  StdIn = request.Project.StdIn,
                                  ProjectType = request.Project.ProjectType
                              };

            foreach (FileEntity fileEntity in request.Project.Files)
            {
                File file = new File();
                foreach (CheckPointEntity checkPointEntity in fileEntity.Checkpoints)
                {
                    file.Checkpoints.Add(new Checkpoint
                                         {
                                             Code = checkPointEntity.Code,
                                             Created = checkPointEntity.Created
                                         });
                }

                file.FileName = fileEntity.FileName;
                file.LastModified = DateTime.Now;
                project.Files.Add(file);
            }

            if (request.Project.ExerciseID is not null)
            {
                Exercise? exercise = await _exerciseRepository.Get(request.Project.ExerciseID);
                if (exercise is not null)
                {
                    project.ExerciseID = exercise.Id;
                }
                else
                {
                    throw new Exception("not allowed");
                }
            }

            ldapUser.Projects.Add(project);

            await _userRepository.UpdateUser(ldapUser);

            return CustomResponse.Success(project.Id);
        }
    }
}