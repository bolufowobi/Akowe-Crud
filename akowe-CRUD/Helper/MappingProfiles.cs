using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using akowe_CRUD.Dtos;
using AutoMapper;
using Domain.Entities;

namespace akowe_CRUD.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AcademicCredential, AcademicCredentialToReturnDto>()
                .ForMember(d => d.TranscriptUrl, o => o.MapFrom<TranscriptUrlResolver<AcademicCredentialToReturnDto>>());
           CreateMap<AcademicCredential, AcademicCredentialShortToReturnDto>()
                .ForMember(d => d.TranscriptUrl, o => o.MapFrom<TranscriptUrlResolver<AcademicCredentialShortToReturnDto>>());
            CreateMap<AcademicCredentialToAddDto, AcademicCredential>();
            CreateMap<AcademicCredentialToUpdateDto, AcademicCredential>();

        }
    }
}
