
namespace BattleShipGame.Core.Helpers
{
    using enums;

    public interface IFireShotResponse
    {
        ShotStatus ShotStatus { get; set; }
        string ShipImpactedName { get; set; }
    }
}