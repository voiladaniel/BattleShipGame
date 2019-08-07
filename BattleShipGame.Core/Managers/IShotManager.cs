
namespace BattleShipGame.Core.Managers
{
    using Helpers;
    using Models;

    public interface IShotManager
    {
        IFireShotResponse FireShot(IPlayer player, ICoordinate coordinate);
    }
}