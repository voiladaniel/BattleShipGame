
namespace BattleShipGame.Core.Managers
{
    using enums;

    public interface IRandomManager
    {
        bool WhoseFirst();
        ShipDirection GetDirection();
        int GetLocation();
    }
}