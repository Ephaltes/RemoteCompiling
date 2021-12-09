using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Command
{
    public class AddFileForProjectCommand  : BaseCommand<int>
    {
       public FileEntity File { get; set; }
       
       public int ProjectId 
       {
           get;
           set;
       }
    }
}