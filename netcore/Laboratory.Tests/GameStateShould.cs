using Laboratory.NetCore.Tests.Models;
using Xunit;
using Xunit.Abstractions;

namespace Laboratory.NetCore.Tests
{
    [Trait("class", "game_state_should")]
    public class GameStateShould : IClassFixture<GameStateFixture>
    {
        private readonly ITestOutputHelper output;
        private readonly GameStateFixture gameStateFixture;

        public GameStateShould(ITestOutputHelper output, GameStateFixture gameStateFixture)
        {
            this.output = output;
            this.gameStateFixture = gameStateFixture;

            this.output.WriteLine(".ctor");
        }

        [Fact]
        public void DamageAllPlayersWhenEarthquake()
        {
            output.WriteLine($"GameState Id={gameStateFixture.State.Id}");

            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            gameStateFixture.State.Players.Add(player1);
            gameStateFixture.State.Players.Add(player2);

            var expectedHealthAfterEarthquake = player1.Health - GameState.EarthquakeDamage;

            gameStateFixture.State.Earthquake();

            Assert.Equal(expectedHealthAfterEarthquake, player1.Health);
            Assert.Equal(expectedHealthAfterEarthquake, player2.Health);
        }

        [Fact]
        public void Reset()
        {
            output.WriteLine($"GameState Id={gameStateFixture.State.Id}");

            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            gameStateFixture.State.Players.Add(player1);
            gameStateFixture.State.Players.Add(player2);

            gameStateFixture.State.Reset();

            Assert.Empty(gameStateFixture.State.Players);
        }


        [Fact]
        public void Dispose()
        {
            this.output.WriteLine("dispose");
        }
    }
}
