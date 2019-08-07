
namespace BattleShipGame.Data.Config
{
    using BattleShipGame.Core.Config;
    using BattleShipGame.Core.Models;

    public class GameConfig : IGameConfig
    {
        public GameConfig(IPlayer player, IPlayer computer)
        {
            Player = player;
            Computer = computer;
        }
        public bool IsPlayerTurn { get; set; }
        public IPlayer Player { get; set; }
        public IPlayer Computer { get; set; }
    }
}