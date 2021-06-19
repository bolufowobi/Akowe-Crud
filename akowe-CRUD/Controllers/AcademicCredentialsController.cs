using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using akowe_CRUD.Dtos;
using akowe_CRUD.Errors;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace akowe_CRUD.Controllers
{
    public class AcademicCredentialsController : BaseApiController
    {
        private readonly ILogger<AcademicCredentialsController> _logger;
        private readonly IAcademicCredentialService _academicCredentialService;
        private readonly IMapper _mapper;

        public AcademicCredentialsController(ILogger<AcademicCredentialsController> logger, 
            IAcademicCredentialService academicCredentialService,
            IMapper mapper)
        {
            _logger = logger;
            _academicCredentialService = academicCredentialService;
            _mapper = mapper;
        }

        [HttpGet("{academicCredentialId:Guid}", Name = nameof(GetAcademicCredential))]
        public async Task<ActionResult<AcademicCredentialToReturnDto>> GetAcademicCredential([FromRoute]Guid academicCredentialId)
        {
            if (academicCredentialId == Guid.Empty)
            {
                return BadRequest(new ApiError(400, "Value for academicCredentialId cannot be empty"));
            }

            var academicCredentialToReturn = await _academicCredentialService.GetAcademicCredentialAsync(academicCredentialId);

            if (academicCredentialToReturn is null)
            {
                return NotFound(new ApiError(404, "Academic Credential not found"));
            }

            return _mapper.Map<AcademicCredentialToReturnDto>(academicCredentialToReturn);

        }


        [HttpGet]
        public async Task<ActionResult<List<AcademicCredentialShortToReturnDto>>> GetAcademicCredentials()
        {
            var academicCredentialToReturn = await _academicCredentialService.GetAcademicCredentialsAsync();

            return _mapper.Map<List<AcademicCredentialShortToReturnDto>>(academicCredentialToReturn);

        }

        [HttpPost]
        public async Task<ActionResult<AcademicCredentialToReturnDto>> CreateAcademicCredential([FromForm] AcademicCredentialToAddDto academicCredentialToAddDto)
        {

            var academicCredentialToReturn = await _academicCredentialService
                .AddAcademicCredentialAsync(_mapper.Map<AcademicCredential>(academicCredentialToAddDto), 
                    academicCredentialToAddDto.TranscriptDoc,
                    HttpContext.Connection.RemoteIpAddress.ToString());

            return CreatedAtRoute(nameof(GetAcademicCredential), new{ academicCredentialId = academicCredentialToReturn.Id}, _mapper.Map<AcademicCredentialToReturnDto>(academicCredentialToReturn));

        }


        [HttpPut("{academicCredentialId:Guid}")]
        public async Task<ActionResult<AcademicCredentialToReturnDto>> UpdateAcademicCredential([FromRoute] Guid academicCredentialId,
            AcademicCredentialToUpdateDto academicCredentialToUpdateDto)
        {
            if (academicCredentialId == Guid.Empty)
            {
                return BadRequest(new ApiError(400, "Value for academicCredentialId cannot be empty"));
            }


            var academicCredentialToReturn = await _academicCredentialService
                .UpdateAcademicCredentialAsync(academicCredentialId, _mapper
                    .Map<AcademicCredential>(academicCredentialToUpdateDto), 
                    HttpContext.Connection.RemoteIpAddress.ToString());

            if (academicCredentialToReturn is null)
            {
                return NotFound(new ApiError(404, "Academic Credential not found"));
            }

            return _mapper.Map<AcademicCredentialToReturnDto>(academicCredentialToReturn);

        }

        [HttpPut("{academicCredentialId:Guid}/UpsertTranscriptFile")]
        public async Task<ActionResult<AcademicCredentialToReturnDto>> UpdateAcademicCredentialTranscriptFile([FromRoute] Guid academicCredentialId,
            IFormFile transcriptFile)
        {
            if (academicCredentialId == Guid.Empty)
            {
                return BadRequest(new ApiError(400, "Value for academicCredentialId cannot be empty"));
            }

            if (transcriptFile == null || transcriptFile.Length < 1)
            {
                return BadRequest(new ApiError(400, "Value for transcript file cannot be empty"));

            }

            var academicCredentialToReturn = await _academicCredentialService
                .UpdateAcademicCredentialTranscriptFileAsync(academicCredentialId, transcriptFile,
                    HttpContext.Connection.RemoteIpAddress.ToString());

            if (academicCredentialToReturn is null)
            {
                return NotFound(new ApiError(404, "Academic Credential not found"));
            }

            return _mapper.Map<AcademicCredentialToReturnDto>(academicCredentialToReturn);

        }

        [HttpDelete("{academicCredentialId:Guid}")]
        public async Task<ActionResult> DeleteAcademicCredential([FromRoute] Guid academicCredentialId)
        {
            if (academicCredentialId == Guid.Empty)
            {
                return BadRequest(new ApiError(400, "Value for academicCredentialId cannot be empty"));
            }

            
            var result = await _academicCredentialService
                .DeleteAcademicCredentialAsync(academicCredentialId,
                    HttpContext.Connection.RemoteIpAddress.ToString());

            if (!result)
            {
                return NotFound(new ApiError(404, "Academic Credential not found"));
            }

            return NoContent();

        }






    }
}
