using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenOpinions.Controllers;
using OpenOpinions.Data;
using OpenOpinions.Dtos;
using OpenOpinions.Models;
using OpenOpinions.Profiles;
using Xunit;

namespace OpenOpinions.UnitTest
{
    public class OpinionControllerTests
    {
        private readonly Mock<IOpinionRepository> _opinionRepository;

        private readonly OpinionController _sut;

        public OpinionControllerTests()
        {
            _opinionRepository = new Mock<IOpinionRepository>();

            var mapper = new MapperConfiguration(x => 
                x.AddProfile(typeof(OpinionsProfiles))).CreateMapper();

            _sut = new OpinionController(_opinionRepository.Object, mapper);
             
        }

        [Fact]
        public async Task hjwhejhj()
        {
            // Arrange
            var dbOpinions = new List<Opinion>
            {
                new Opinion
                {
                    Id = 1,
                    Text = "opinion1"
                },
                new Opinion
                {
                    Id = 2,
                    Text = "opinion2"
                }
            };

            _opinionRepository
                .Setup(x => x.GetAllOpinions())
                .ReturnsAsync(dbOpinions);


            // Act
            var actionResult = await _sut.GetAllOpinions();

            // Assert
            var result = actionResult.Result as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
            var dtoCollection = Assert.IsAssignableFrom<IEnumerable<ReadOpinionDto >>(result.Value);
            Assert.NotEmpty(dtoCollection);

        }
    }
}
