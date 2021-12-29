using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class AddProjectHandler : BaseHandler<AddProjectCommand, CustomResponse<int>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IExerciseRepository _exerciseRepository;
        public AddProjectHandler(IUserRepository userRepository, IExerciseRepository exerciseRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
        }

        public override async Task<CustomResponse<int>> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = _userRepository.GetUserByLdapUid(ldapIdent);

            Project project = new Project
                              {
                                  ProjectName = request.Project.ProjectName,
                                  StdIn = request.Project.StdIn,
                                  ProjectType = request.Project.ProjectType
                              };
            foreach (FileEntity projectFiles in request.Project.Files)
            {
                var x = new File();
                foreach (var VARIABLE in projectFiles.Checkpoints)
                {
                    x.Checkpoints.Add(new Checkpoint()
                                      {
                                          Code = VARIABLE.Code,
                                          Created = VARIABLE.Created
                                      });
                }
                x.FileName = projectFiles.FileName;
                x.LastModified = DateTime.Now;
                project.Files.Add(x);
            }

            if (request.Project.ExerciseID is not null)
            {
               var x = _exerciseRepository.Get(request.Project.ExerciseID);
               if (x is not null)
               {
                   project.ExerciseID = x.Id;
               }
               else
               {
                   throw new Exception("not allowed");
               }
            }
                
            ldapUser.Projects.Add(project);

            _userRepository.UpdateUser(ldapUser);

            return CustomResponse.Success(project.Id);
        }
    }
}