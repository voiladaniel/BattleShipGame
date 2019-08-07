
namespace BattleShipGame.Core.Managers
{
    using Helpers;
    using Models;

    public interface IShipManager
    {
        void PlaceShipsOnBoard(IPlayer player);
        void CheckShipsForHit(ICoordinate coordinate, IFireShotResponse response, IPlayer player);
    }
}