using System.Collections.Generic;
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
        public ActionResult<IEnumerable<ReadOpinionDto>> GetAllOpinions()
        {
            var allOpinions = _repository.GetAllOpinions();
            return Ok(_mapper.Map<IEnumerable<ReadOpinionDto>>(allOpinions));
        }

        [HttpGet("{id}", Name = "GetOpinionById")]
        public ActionResult<ReadOpinionDto> GetOpinionById(int id)
        {
            var opinion = _repository.GetOpinionById(id);
            if (opinion != null)
            {
                return Ok(_mapper.Map<ReadOpinionDto>(opinion));
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult<ReadOpinionDto> CreateOpinion(CreateOpinionDto createOpinionDto)
        {

            var opinionModel = _mapper.Map<Opinion>(createOpinionDto);
            _repository.CreateOpinion(opinionModel);
            var opinionReadDto = _mapper.Map<ReadOpinionDto>(opinionModel);
            _repository.SaveChanges();
            return CreatedAtRoute(nameof(GetOpinionById), new{Id=opinionModel.Id}, opinionReadDto );
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOpinion(int id)
        {
            var opinion = _repository.GetOpinionById(id);
            if (opinion != null)
            {
                _repository.DeleteOpinion(opinion);
                _repository.SaveChanges();
            }
            return NoContent();
             
        }

    } 
}
