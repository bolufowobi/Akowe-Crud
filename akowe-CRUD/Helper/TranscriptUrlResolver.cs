using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using akowe_CRUD.Dtos;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace akowe_CRUD.Helper
{
    public class TranscriptUrlResolver<T>: IValueResolver<AcademicCredential, T, string>
    {
        private readonly IConfiguration _config;

        public TranscriptUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(AcademicCredential source, T destination, string destMember,
            ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.TranscriptUrl))
            {
                return $"{_config["ApiUrl"]}{source.TranscriptUrl}";
            }

            return $"{_config["ApiUrl"]}Contents/Transcripts/Placeholder.png";
        }
    }
}
