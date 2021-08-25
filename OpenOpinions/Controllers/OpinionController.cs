using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OpenOpinions.Data;
using OpenOpinions.Models;

namespace OpenOpinions.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OpinionController : ControllerBase
    {
        private readonly IOpinionRepository _repository;

        public OpinionController(IOpinionRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Opinion>> GetAllOpinions()
        {
            var allOpinions = _repository.GetAllOpinions();
            return Ok(allOpinions);
        }

        [HttpGet("{id}")]
        public ActionResult<Opinion> GetOpinionById(int id)
        {
            var opinion = _repository.GetOpinionById(id);
            return Ok(opinion);
        }
    } 
}
