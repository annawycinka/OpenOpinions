using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OpenOpinions.Data;
using OpenOpinions.Dtos;
using OpenOpinions.Models;
using SQLitePCL;

namespace OpenOpinions.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OpinionController : ControllerBase
    {
        private readonly IOpinionRepository _repository;
        private readonly IMapper _mapper;

        public OpinionController(IOpinionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadOpinionDto>>> GetAllOpinions()
        {
            var allOpinions = await _repository.GetAllOpinions();
            return Ok(_mapper.Map<IEnumerable<ReadOpinionDto>>(allOpinions));
        }

        [HttpGet("{id}", Name = "GetOpinionById")]
        public async Task<ActionResult<ReadOpinionDto>> GetOpinionById(int id)
        {
            var opinion = await _repository.GetOpinionById(id);
            if (opinion != null)
            {
                return Ok(_mapper.Map<ReadOpinionDto>(opinion));
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<ReadOpinionDto>> CreateOpinion(CreateOpinionDto createOpinionDto)
        {
            var opinionModel = _mapper.Map<Opinion>(createOpinionDto);
            await _repository.CreateOpinion(opinionModel);
            var opinionReadDto = _mapper.Map<ReadOpinionDto>(opinionModel);
            return CreatedAtRoute(nameof(GetOpinionById), new{Id=opinionModel.Id}, opinionReadDto );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOpinion(int id)
        {
            var opinion = await _repository.GetOpinionById(id);
            if (opinion != null)
            {
                await _repository.DeleteOpinion(opinion);
            }
            return NoContent();
             
        }

    } 
}
