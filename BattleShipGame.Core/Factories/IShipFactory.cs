
namespace BattleShipGame.Core.Factories
{
    using BattleShipGame.Core.enums;
    using BattleShipGame.Core.Models;

    public interface IShipFactory
    {
        IShip CreateShip(ShipType type);
    }
}