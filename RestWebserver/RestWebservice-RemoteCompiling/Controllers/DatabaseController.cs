using System;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/Database")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    public class DatabaseController
    {
        private readonly IMediator _mediator;
        private readonly UserRepository _userRepository;

        public DatabaseController(IMediator mediator, RemoteCompileDbContext context)
        {
            _mediator = mediator;
            _userRepository = new UserRepository(context);
        }
        [HttpPost("AddUser")]
        public IActionResult AddUser(User newUser)
        {
            _userRepository.AddUser(newUser);

            return CustomResponse.Success("yey").ToResponse();
        }
        [HttpPost("AddFileForUser")]
        public IActionResult AddFileForUser(string ldapIdent, File newFile)
        {
            User? findUser = _userRepository.GetUserByLdapUid(ldapIdent);
            findUser.Files.Add(newFile);
            _userRepository.UpdateUser(findUser);

            return CustomResponse.Success("yey").ToResponse();
        }
        [HttpPost("AddCheckpointForFile")]
        public IActionResult AddCheckpointForFile(string ldapIdent, int fileId, Checkpoint checkpoint)
        {
            User? findUser = _userRepository.GetUserByLdapUid(ldapIdent);
            bool flag = false;
            foreach (var i in findUser.Files)
                if (i.Id == fileId)
                {
                    i.LastModified = DateTime.Now;
                    i.Checkpoints.Add(checkpoint);
                    flag = true;
                }

            if (!flag)
                return CustomResponse.Error<string>(401, "File not found").ToResponse();

            _userRepository.UpdateUser(findUser);

            return CustomResponse.Success("yey").ToResponse();
        }


        [HttpPut("UpdateFileForUser")]
        public IActionResult UpdateFileForUser(string ldapIdent, int fileId, File newFile)
        {
            User? findUser = _userRepository.GetUserByLdapUid(ldapIdent);


            bool flag = false;
            foreach (var i in findUser.Files)
                if (i.Id == fileId)
                {
                    i.LastModified = DateTime.Now;
                    i.FileName = newFile.FileName;
                    flag = true;
                }

            if (!flag)
                return CustomResponse.Error<string>(401, "File not found").ToResponse();

            _userRepository.UpdateUser(findUser);

            return CustomResponse.Success("yey").ToResponse();
        }


        [HttpDelete("RemoveFileForUser")]
        public IActionResult RemoveFileForUser(string ldapIdent, int fileId)
        {
            User? findUser = _userRepository.GetUserByLdapUid(ldapIdent);

            bool deleted = false;

            foreach (File userFile in findUser.Files)
            {
                if (userFile.Id != fileId)
                    continue;

                findUser.Files.Remove(userFile);
                deleted = true;

                break;
            }

            if (!deleted)
                return CustomResponse.Error<string>(401, "File not found").ToResponse();

            _userRepository.UpdateUser(findUser);

            return CustomResponse.Success("yey").ToResponse();
        }
        [HttpDelete("RemoveCheckpointForFile")]
        public IActionResult RemoveCheckpointForFile(string ldapIdent, int fileId, int checkpointId)
        {
            User? findUser = _userRepository.GetUserByLdapUid(ldapIdent);

            bool deleted = false;
            foreach (File file in findUser.Files)
            {
                file.LastModified = DateTime.Now;

                foreach (Checkpoint checkpoint in file.Checkpoints)
                {
                    if (checkpoint.Id != checkpointId)
                        continue;

                    file.Checkpoints.Remove(checkpoint);
                    deleted = true;

                    break;
                }
            }

            if (!deleted)
                return CustomResponse.Error<string>(401, "File not found").ToResponse();

            _userRepository.UpdateUser(findUser);

            return CustomResponse.Success("yey").ToResponse();
        }


        [HttpGet("getUser")]
        public IActionResult GetUser(string ldapIdent)
        {
            return CustomResponse.Success(_userRepository.GetUserByLdapUid(ldapIdent)).ToResponse();
        }

        internal void CreateNewUser(LdapUser ldapUser)
        {
            User? newUser = new User();
            newUser.LdapUid = ldapUser.Uid;
            newUser.Email = ldapUser.Mail;
            newUser.Name = ldapUser.GivenName;

            //todo switch ldap user to internal user
            newUser.UserRole = UserRole.DefaultUser;

            _userRepository.AddUser(newUser);
        }
        internal bool UpdateUserData(User user)
        {
            // todo verify that files and checkpoints get stored anyway without loading 
            // TODO: userFromDB could be null
            User userFromDb = _userRepository.GetUserByLdapUid(user.LdapUid);
            if (userFromDb is null)
            {
                return false;
            }
            userFromDb.Email = user.Email;
            userFromDb.Name = user.Name;
            userFromDb.UserRole = user.UserRole;

            _userRepository.UpdateUser(userFromDb);
            return true;
        }
    }
}