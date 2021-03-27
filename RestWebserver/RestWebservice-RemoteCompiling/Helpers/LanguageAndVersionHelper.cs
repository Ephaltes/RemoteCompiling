using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;

namespace RestWebservice_RemoteCompiling.Helpers
{
    public interface ILanguageAndVersionHelper
    {
        bool CheckConfigurationForVersionAndLanguage(string language, string version);
        public List<string> GetSupportedLanguages();
        public List<string> GetSupportedVersionsForLanguage(string language);
    }
    public class LanguageAndVersionHelper : ILanguageAndVersionHelper
    {
        private readonly IConfiguration _Configuration;

        public LanguageAndVersionHelper(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public bool CheckConfigurationForVersionAndLanguage(string language, string version)
        {
            bool isValidVersion = false;
            var item = _Configuration.GetSection("supportedVersions").GetSection(language).GetChildren() ?? throw new NullReferenceException();
            item.ToList().ForEach(x =>
            {
                if (x.Value == version)
                {
                    isValidVersion = true;
                }
            });
            return isValidVersion;
        }
        public List<string> GetSupportedLanguages()
        {
            List<string> returnList = new List<string>();
            var item = _Configuration.GetSection("supportedVersions").GetChildren().ToList();
            item.ForEach(x => { returnList.Add(x.Key);Console.WriteLine(x); }) ;
            return returnList;
        }
        public List<string> GetSupportedVersionsForLanguage(string language)
        {
            List<string> returnList = new List<string>();
            var item = _Configuration.GetSection("supportedVersions").GetSection(language).GetChildren().ToList() ?? throw new NullReferenceException();
            item.ForEach(x => returnList.Add(x.Value));
            return returnList;
        }
    }
}
