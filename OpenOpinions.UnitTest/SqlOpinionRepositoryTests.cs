using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenOpinions.Data;
using OpenOpinions.Models;
using Xunit;

namespace OpenOpinions.UnitTest
{
    public class SqlOpinionRepositoryTests
    {
        private readonly IOpinionRepository _sut;

        public SqlOpinionRepositoryTests()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<OpinionContext>(x => 
                x.UseInMemoryDatabase($"InMemorySQL-{Guid.NewGuid()}"));
            serviceCollection.AddScoped<IOpinionRepository, SqlOpinionRepository>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _sut = serviceProvider.GetRequiredService<IOpinionRepository>();
        }

        [Fact]
        public async Task WhenInitializedRepository_ThenEmpty()
        {
            // Act
            var opinions = await _sut.GetAllOpinions();

            // Assert
            Assert.Empty(opinions);
        }

        [Fact]
        public async Task WhenAddOpinion_ThenAddedAndIdGenerated()
        {
            // Arrange
            var opinion = new Opinion
            {
                Text = "some opinion"
            };

            // Act
            await _sut.CreateOpinion(opinion);
            var opinions = await _sut.GetAllOpinions();

            // Assert
            Assert.Equal(1, opinion.Id);
            Assert.Single(opinions);
        }

        [Fact]
        public async Task WhenDeleteOpinion_ThenOpinionDeleted()
        {
            //Arrange
            var opinion = new Opinion
            {
                Text = "some opinion"
            };

            //Act
            await _sut.CreateOpinion(opinion);
            var opinionToDelete = await _sut.GetOpinionById(opinion.Id);
            await _sut.DeleteOpinion(opinion);
            

            //Assert
            var allOpinions = await _sut.GetAllOpinions();
            Assert.DoesNotContain(opinionToDelete, allOpinions);

        }

        [Fact]
        public async Task WhenGetAllOpinions_ThenAllOpinionsGiven()
        {
            //Arrange
            var opinion1 = new Opinion
            {
                Text = "first opinion"
            };
            var opinion2 = new Opinion
            {
                Text = "second opinion"
            };

            //Act
            await _sut.CreateOpinion(opinion1);
            await _sut.CreateOpinion(opinion2);
            var allOpinions = await _sut.GetAllOpinions();
            var opinionsList = allOpinions.ToList();

            //Assert
            Assert.Contains(opinion1, opinionsList);
            Assert.Contains(opinion2, opinionsList);
            Assert.Equal(2, opinionsList.Count);
        }


        [Fact]
        public async Task WhenGetOpinionById_ThenGetOpinion()
        {
            //Arrange
            var opinion1 = new Opinion
            {
                Text = "first opinion"
            };
            var opinion2 = new Opinion
            {
                Text = "second opinion"
            };

            //Act
            await _sut.CreateOpinion(opinion1);
            await _sut.CreateOpinion(opinion2);
            var opinionGiven2 = await _sut.GetOpinionById(opinion2.Id);
            var opinionGiven1 = await _sut.GetOpinionById(opinion1.Id);

            //Assert
            Assert.Equal(opinion2, opinionGiven2);
            Assert.Equal(opinion1, opinionGiven1);
        }
    }
}
