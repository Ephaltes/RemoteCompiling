using RestWebService_StaticCodeAnalysis.Services.Entities.Enums;

using Serilog;

using System.Collections.Generic;
using System.Xml;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents
{
    public class ValgrindXmlReportParser : IValgrindReportParser
    {
        private readonly ILogger _logger;

        public ValgrindXmlReportParser(ILogger logger)
        {
            _logger = logger;
        }

        public List<Services.Entities.Issue> ReadIssues(XmlDocument document)
        {
            var issues = new List<Services.Entities.Issue>();
            var valgrindOutput = document.LastChild;

            _logger.Information($"Valgrind outut tag has {valgrindOutput.ChildNodes.Count} child nodes");

            foreach (XmlNode tag in valgrindOutput.ChildNodes)
            {
                _logger.Information($"Tag name: {tag.Name}");

                switch (tag.Name)
                {
                    case "error":
                        issues.Add(ReadErrorTag(tag));
                        break;
                    case "fatal_signal":
                        issues.Add(ReadFatalSignalTag(tag));
                        break;
                }
            }

            return issues;
        }

        private Services.Entities.Issue ReadFatalSignalTag(XmlNode fatalSignalTag)
        {
            var line = 0;
            string message = string.Empty;
            var severity = IssueSeverity.MAJOR;
            var type = IssueType.BUG;

            foreach (XmlNode tag in fatalSignalTag.ChildNodes)
            {
                switch (tag.Name)
                {
                    case "event":
                        message = tag.InnerText;
                        break;

                    case "stack":
                        line = ReadLineFromStackTag(tag);
                        break;
                }
            }

            return new Services.Entities.Issue
            {
                Component = "main.c",
                Message = message,
                Severity = severity,
                TextLocation = new Services.Entities.TextLocation
                {
                    StartLine = line,
                    EndLine = line,
                    StartOffset = 0,
                    EndOffset = 0
                },
                Type = type
            };
        }

        private Services.Entities.Issue ReadErrorTag(XmlNode errorTag)
        {
            var line = 0;
            string message = string.Empty;
            var severity = IssueSeverity.MINOR;
            var type = IssueType.CODE_SMELL;

            _logger.Information("Reading error tag");

            foreach (XmlNode tag in errorTag.ChildNodes)
            {
                _logger.Information($"Reading sub-error tag {tag.Name}");

                switch (tag.Name)
                {
                    case "kind":
                        if (tag.InnerText.StartsWith("Leak_"))
                        {
                            type = IssueType.BUG;
                        }
                        break;

                    case "what":
                        message = tag.InnerText;
                        break;

                    case "xwhat":
                        foreach (XmlNode childNode in tag.ChildNodes)
                        {
                            if (childNode.Name == "text")
                            {
                                message = childNode.InnerText;
                            }
                        }
                        break;

                    case "stack":
                        line = ReadLineFromStackTag(tag);
                        break;
                }
            }

            return new Services.Entities.Issue
            {
                Component = "main.c",
                Message = message,
                Severity = severity,
                TextLocation = new Services.Entities.TextLocation
                {
                    StartLine = line,
                    EndLine = line,
                    StartOffset = 0,
                    EndOffset = 0
                },
                Type = type
            };
        }

        private int ReadLineFromStackTag(XmlNode stackTag)
        {
            int line = 0;

            foreach (XmlNode childNode in stackTag.ChildNodes)
            {
                _logger.Information($"Reading sub-stack tag {childNode.Name}");

                if (childNode.Name != "frame")
                {
                    continue;
                }

                int frameLine = 0;
                bool isFromInternalFile = false;

                foreach (XmlNode frameNode in childNode.ChildNodes)
                {
                    _logger.Information($"Reading sub-frame node {frameNode.Name}");

                    if (frameNode.Name == "file" && frameNode.InnerText == "main.c")
                    {
                        _logger.Information("Found frame node for main.c");
                        isFromInternalFile = true;
                    }

                    if (frameNode.Name == "line")
                    {
                        _logger.Information($"Sub-frame node line number is {frameNode.InnerText}");
                        frameLine = int.Parse(frameNode.InnerText);
                    }
                }

                if (isFromInternalFile)
                {
                    line = frameLine;
                    break;
                }
            }

            return line;
        }
    }
}
