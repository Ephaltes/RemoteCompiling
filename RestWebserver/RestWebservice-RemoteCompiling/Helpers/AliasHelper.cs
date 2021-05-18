using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RestWebservice_RemoteCompiling.Helpers
{
    public class AliasHelper : IAliasHelper
    {
        private readonly Dictionary<string, List<string>> _AliasMap;

        public AliasHelper()
        {
            if (FileSystem.FileExists("AliasMap.json"))
            {
                Log.Information("AliasMap.json loaded");
                _AliasMap = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(FileSystem.ReadAllText("AliasMap.json"));
            }
            else
            {
                Log.Error("AliasMap.json could not be loaded from default working directory");
                _AliasMap = new Dictionary<string, List<string>>();
            }
            
        }
        public string GetAlias(string FindAliasForMe)
        {
            foreach(var item in _AliasMap){
                if(item.Key == FindAliasForMe)
                {
                    return item.Key;
                }
                foreach(var NestedAlias in item.Value)
                {
                    if(NestedAlias == FindAliasForMe)
                    {
                        return item.Key;
                    }
                }
            }
            return null;
        }
    }
}
