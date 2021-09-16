using OpenOpinions.Data;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OpenOpinions.Models;
using Xunit;

namespace OpenOpinions.UnitTest
{
    public class InMemoryOpinionRepositoryTests
    {
        private readonly InMemoryOpinionRepository _sut;

        public InMemoryOpinionRepositoryTests()
        {
            _sut = new InMemoryOpinionRepository();
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
            //Arrangedddddd
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
