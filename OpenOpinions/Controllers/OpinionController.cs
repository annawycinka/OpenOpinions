using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OpenOpinions.Data;
using OpenOpinions.Dtos;
using OpenOpinions.Models;

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

        [HttpGet("{id}")]
        public ActionResult<ReadOpinionDto> GetOpinionById(int id)
        {
            var opinion = _repository.GetOpinionById(id);
            if (opinion != null)
            {
                return Ok(_mapper.Map<ReadOpinionDto>(opinion));
            }
            return NotFound();
        }
    } 
}
