
namespace BattleShipGame.Data.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.enums;
    using BattleShipGame.Core.Helpers;
    using BattleShipGame.Core.Models;

    public class Board : IBoard
    {
        public const int XCoordinator = 10;

        public const int YCoordinator = 10;
        
        public Dictionary<ICoordinate, ShotHistory> ShotHistoryList { get; set; }
        
        public IShip[] Ships { get; set;  }
        
        public Board()
        {
            //_shotHistory = shotHistory;
            Ships = new IShip[3];
            ShotHistoryList = new Dictionary<ICoordinate, ShotHistory>();
        }
        public void CheckForVictory(IFireShotResponse response, IPlayer player)
        {
            if (response.ShotStatus == ShotStatus.HitAndSunk)
            {
                if (player.Board.Ships.All(s => s.IsSunk))
                    response.ShotStatus = ShotStatus.Victory;
            }
        }
        public ShotHistory CheckCoordinate(ICoordinate coordinate)
        {
            if (ShotHistoryList.ContainsKey(coordinate))
            {
                return ShotHistoryList[coordinate];
            }
            else
            {
                return ShotHistory.Unknown;
            }
        }

        public bool IsValidCoordinate(ICoordinate coordinate, ShipDirection shipDirection, int shipLength)
        {
            switch (shipDirection)
            {
                case ShipDirection.Up:
                    return coordinate.YCoordinate <= ((YCoordinator - shipLength) + 1);
                case ShipDirection.Down:
                    return coordinate.YCoordinate >= shipLength;
                case ShipDirection.Right:
                    return (coordinate.XCoordinate <= (XCoordinator - shipLength) + 1);
                case ShipDirection.Left:
                    return (coordinate.XCoordinate >= shipLength);
                default:
                    return false;
            }
        }
        public bool IsCoordinateOnBoard(ICoordinate coordinate)
        {
            return coordinate.XCoordinate >= 1 && coordinate.XCoordinate <= XCoordinator &&
                   coordinate.YCoordinate >= 1 && coordinate.YCoordinate <= YCoordinator;
        }
        public bool IsOverlap(IShip[] ships, ICoordinate coordinate)
        {
            foreach (var ship in ships)
            {
                if (ship != null)
                {
                    foreach (var existingCoordinate in ship.Coordinates)
                    {
                        if (existingCoordinate.Equals(coordinate))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
