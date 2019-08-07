
namespace BattleShipGame.Data.Helpers
{
    using Core.enums;
    using BattleShipGame.Core.Helpers;

    public class FireShotResponse : IFireShotResponse
    {
        public ShotStatus ShotStatus { get; set; }
        public string ShipImpactedName { get; set; }
    }
}
