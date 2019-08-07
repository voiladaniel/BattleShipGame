
namespace BattleShipGame.Data.Models
{
    using Core.enums;
    using BattleShipGame.Core.Models;

    public class Ship : IShip
    {
        public ShipType ShipType { get; set; }

        public ShipDirection Direction { get; set; }
        public string ShipName => ShipType.ToString();
        
        public ICoordinate[] Coordinates { get; set; }

        public int LifeRemaining { get; set; }
        public bool IsSunk => LifeRemaining == 0;

        public Ship(ShipType shipType, int numberOfSquares)
        {
            ShipType = shipType;
            LifeRemaining = numberOfSquares;
            Coordinates = new ICoordinate[numberOfSquares];
        }
    }
}