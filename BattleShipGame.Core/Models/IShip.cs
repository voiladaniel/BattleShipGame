
namespace BattleShipGame.Core.Models
{
    using enums;

    public interface IShip
    {
        ShipType ShipType { get; set; }
        ICoordinate[] Coordinates { get; set; }
        ShipDirection Direction { get; set; }
        string ShipName { get; }
        bool IsSunk { get; }
        int LifeRemaining { get; set; }
    }
}