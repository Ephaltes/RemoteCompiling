using System.Collections.Generic;
using System.Xml;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents
{
    public interface IValgrindReportParser
    {
        public List<Services.Entities.Issue> ReadIssues(XmlDocument document, string mainFile);
    }
}