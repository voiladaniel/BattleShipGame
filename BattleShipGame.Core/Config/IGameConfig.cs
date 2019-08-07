
namespace BattleShipGame.Core.Config
{
    using Models;

    public interface IGameConfig
    {
        bool IsPlayerTurn { get; set; }
        IPlayer Player { get; set; }
        IPlayer Computer { get; set; }
    }
}