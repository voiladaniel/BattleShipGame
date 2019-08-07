
namespace BattleShipGame.BusinessLogic.Factories
{
    using Core.enums;
    using BattleShipGame.Core.Factories;
    using Core.Models;
    using Data.Models;

    public class ShipFactory : IShipFactory
    {
        public IShip CreateShip(ShipType type)
        {
            switch (type)
            {
                case ShipType.Destroyer:
                    return new Ship(ShipType.Destroyer, 2);
                case ShipType.Battleship:
                    return new Ship(ShipType.Battleship, 4);
                default:
                    return null;
            }
        }
    }
}
