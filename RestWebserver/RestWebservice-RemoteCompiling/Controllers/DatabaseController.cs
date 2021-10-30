using System;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/Database")] 
    [ApiController] 
    [EnableCors("AllAllowedPolicy")]
    public class DatabaseController
    {
        private readonly IMediator _mediator;
        private readonly Repository _repository;
        
        public DatabaseController(IMediator mediator,RemoteCompileDbContext context)
        {
            _mediator = mediator;
            _repository = new Repository(context);
        }
        [HttpPost("AddUser")]
        public IActionResult AddFileForUser(User newUser)
        {
            _repository.AddUser(newUser);
            return CustomResponse.Success("yey").ToResponse();
        }
        [HttpPost("AddFileForUser")]
        public IActionResult AddFileForUser(string ldapIdent,File newFile)
        {
            var findUser = _repository.GetUserByLdapIdent(ldapIdent);
            findUser.Files.Add(newFile);
            _repository.UpdateUser(findUser);
            return CustomResponse.Success("yey").ToResponse();
        }
        [HttpPost("AddCheckpointForFile")]
        public IActionResult AddFileForUser(string ldapIdent,int fileId,Checkpoint checkpoint)
        {
            var findUser = _repository.GetUserByLdapIdent(ldapIdent);
            bool flag = false;
            foreach (var i in findUser.Files)
            {
                if (i.Id == fileId)
                {
                    i.LastModified = DateTime.Now;
                    i.Checkpoints.Add(checkpoint);
                    flag = true;
                }
            }

            if (!flag)
            {
                return CustomResponse.Error<string>(401, "File not found").ToResponse();
            }
            _repository.UpdateUser(findUser);
            return CustomResponse.Success("yey").ToResponse();
        }
        
        
        
        
        
        [HttpPut("UpdateFileForUser")]
        public IActionResult UpdateFileForUser(string ldapIdent,int fileId,File newFile)
        {
            var findUser = _repository.GetUserByLdapIdent(ldapIdent);
            
            
            bool flag = false;
            foreach (var i in findUser.Files)
            {
                if (i.Id == fileId)
                {
                    i.LastModified = DateTime.Now;
                    i.FileName = newFile.FileName;
                    flag = true;
                }
            }

            if (!flag)
            {
                return CustomResponse.Error<string>(401, "File not found").ToResponse();
            }

            _repository.UpdateUser(findUser);
            return CustomResponse.Success("yey").ToResponse();
        }
        
        
        
        [HttpDelete("RemoveFileForUser")]
        public IActionResult RemoveFileForUser(string ldapIdent,int fileId)
        {
            var findUser = _repository.GetUserByLdapIdent(ldapIdent);
            
            bool deleted = false;
            for (int i = 0; i < findUser.Files.Count; i++)
            {
                if (findUser.Files[i].Id == fileId)
                {
                    findUser.Files.RemoveAt(i);
                    deleted = true;
                    break;
                }
            }

            if (!deleted)
            {
                return CustomResponse.Error<string>(401, "File not found").ToResponse();
            }
            
            _repository.UpdateUser(findUser);
            return CustomResponse.Success("yey").ToResponse();
        }
        [HttpDelete("RemoveCheckpointForFile")]
        public IActionResult RemoveCheckpointForFile(string ldapIdent,int fileId,int checkpointId)
        {
            var findUser = _repository.GetUserByLdapIdent(ldapIdent);
            
            bool deleted = false;
            foreach (var x in findUser.Files)
            {
                x.LastModified = DateTime.Now;
                for (int i = 0; i < x.Checkpoints.Count; i++)
                {
                    if (x.Checkpoints[i].Id == checkpointId)
                    {
                        x.Checkpoints.RemoveAt(i);
                        deleted = true;
                        break;
                    }
                } 
            }
            
            if (!deleted)
            {
                return CustomResponse.Error<string>(401, "File not found").ToResponse();
            }
            
            _repository.UpdateUser(findUser);
            return CustomResponse.Success("yey").ToResponse();
        }
        
        
        
        [HttpGet("getUser")]
        public IActionResult GetUser(string ldapIdent)
        {
            return CustomResponse.Success(_repository.GetUserByLdapIdent(ldapIdent)).ToResponse();
        }
    }
}