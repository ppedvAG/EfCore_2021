using Moq;
using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Contracts;
using System;
using System.Linq;
using Xunit;

namespace ppedv.GMEStore.Logic.Tests
{
    public class CoreTests
    {
        [Fact]
        public void GetCompanyThatPublisheMostGamesOfYear_negative_year()
        {
            var core = new Core(null);

            Assert.Throws<ArgumentException>(() => core.GetCompanyThatPublisheMostGamesOfYear(-1));
        }


        [Fact]
        public void GetCompanyThatPublisheMostGamesOfYear_2_publishers_p2_has_more_games_TestRepo()
        {
            var core = new Core(new TestUoW());

            var result = core.GetCompanyThatPublisheMostGamesOfYear(1);

            Assert.Equal("P2", result.Name);

        }

        [Fact]
        public void GetCompanyThatPublisheMostGamesOfYear_2_publishers_p2_has_more_games_MOQ()
        {
            var gameRepoMock = new Mock<IGameRepository>();
            gameRepoMock.Setup(x => x.QueryGamesIncludingAll())
                .Returns(() =>
                {
                    var p1 = new Company() { Name = "P1" };
                    var p2 = new Company() { Name = "P2" };

                    p1.Published.Add(new Game() { Publisher = p1 });
                    p1.Published.Add(new Game() { Publisher = p1 });

                    p2.Published.Add(new Game() { Publisher = p2 });
                    p2.Published.Add(new Game() { Publisher = p2 });
                    p2.Published.Add(new Game() { Publisher = p2 });

                    return new[] { p1, p2 }.SelectMany(x => x.Published).AsQueryable();
                });

            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.GameRepository).Returns(gameRepoMock.Object);
            var core = new Core(uowMock.Object);


            var result = core.GetCompanyThatPublisheMostGamesOfYear(1);

            Assert.Equal("P2", result.Name);

            gameRepoMock.Verify(x => x.QueryGamesIncludingAll(), Times.Once);
        }

        [Fact]
        public void GetCompanyThatPublisheMostGamesOfYear_empty_db_return_null()
        {
            var gameRepoMock = new Mock<IGameRepository>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.GameRepository).Returns(gameRepoMock.Object);

            var core = new Core(uowMock.Object);

            var result = core.GetCompanyThatPublisheMostGamesOfYear(1);

            Assert.Null(result);
        }
    }
}
