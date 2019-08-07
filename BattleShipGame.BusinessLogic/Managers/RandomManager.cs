
namespace BattleShipGame.BusinessLogic.Managers
{
    using System;
    using Core.enums;
    using BattleShipGame.Core.Managers;

    public class RandomManager : IRandomManager
    {
        public static Random Random = new Random();

        public bool WhoseFirst()
        {
            return Random.Next(1, 10) <= 5;
        }

        public ShipDirection GetDirection()
        {
            switch (Random.Next(1, 5))
            {
                case 1:
                    return ShipDirection.Left;
                case 2:
                    return ShipDirection.Right;
                case 3:
                    return ShipDirection.Down;
                case 4:
                    return ShipDirection.Up;
                default:
                    return ShipDirection.Up;
            }
        }

        public int GetLocation()
        {
            return Random.Next(1, 11);
        }
    }
}
