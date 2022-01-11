using AutoMapper;

using RestWebservice_StaticCodeAnalysis.DTOs;

using RestWebService_StaticCodeAnalysis.Services.Entities;

namespace RestWebservice_StaticCodeAnalysis.MappingProfiles
{
    public class ScanProfile : Profile
    {
        public ScanProfile()
        {
            CreateMap<Scan, ScanDto>().ReverseMap();
            CreateMap<ScanJob, ScanJobDto>().ReverseMap();
            CreateMap<Issue, IssueDto>().ReverseMap();
            CreateMap<TextLocation, TextLocationDto>().ReverseMap();
        }
    }
}
