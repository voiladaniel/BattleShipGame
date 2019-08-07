
namespace BattleShipGame.Core.Models
{
    using System.Collections.Generic;
    using enums;
    using Helpers;

    public interface IBoard
    {
        IShip[] Ships { get; }

        ShotHistory CheckCoordinate(ICoordinate coordinate);

        Dictionary<ICoordinate, ShotHistory> ShotHistoryList { get; set; }

        bool IsValidCoordinate(ICoordinate coordinate, ShipDirection shipDirection, int shipLength);
        bool IsOverlap(IShip[] ships, ICoordinate coordinate);
        void CheckForVictory(IFireShotResponse response, IPlayer player);

        bool IsCoordinateOnBoard(ICoordinate coordinate);
    }
}