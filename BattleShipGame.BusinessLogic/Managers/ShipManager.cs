
namespace BattleShipGame.BusinessLogic.Managers
{
    using System;
    using System.Linq;
    using Core.enums;
    using BattleShipGame.Core.Factories;
    using BattleShipGame.Core.Helpers;
    using BattleShipGame.Core.Managers;
    using Core.Models;
    using Data.Models;
    using log4net;

    public class ShipManager : IShipManager
    {
        private IShip _ship;

        private readonly IShipFactory _shipFactory;

        private ICoordinate _coordinate;

        private readonly ICoordinateManager _coordinateManager;

        private readonly IRandomManager _randomManager;

        private ShipDirection _shipDirection;

        private readonly IOutputManager _outputManager;

        private readonly IBoard _board;

        private readonly ILog _log;

        public ShipManager(IShipFactory shipFactory, 
            ICoordinateManager coordinateManager, 
            IRandomManager randomManager, 
            IOutputManager outputManager,
            IBoard board,
            ILog log)
        {
            _shipFactory = shipFactory ?? throw new ArgumentNullException(nameof(shipFactory));
            _coordinateManager = coordinateManager ?? throw new ArgumentNullException(nameof(coordinateManager));
            _randomManager = randomManager ?? throw new ArgumentNullException(nameof(randomManager));
            _outputManager = outputManager ?? throw new ArgumentNullException(nameof(outputManager));
            _board = board ?? throw new ArgumentNullException(nameof(board));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }
        public void PlaceShipsOnBoard(IPlayer player)
        {
            try
            {
                _log.Info("BTG - trying to place ships on board!");

                var ships = new[] { ShipType.Battleship, ShipType.Destroyer, ShipType.Destroyer };
                var i = 0;
                foreach (var ship in ships)
                {
                    _ship = _shipFactory.CreateShip(ship);
                    _shipDirection = _randomManager.GetDirection();
                    do
                    {
                        _coordinate = _coordinateManager.GetRandomCoordinate();
                    }
                    while (!_board.IsValidCoordinate(_coordinate, _shipDirection, _ship.Coordinates.Length)
                           || _board.IsOverlap(player.Board.Ships, _coordinate));

                    _ship = PlaceShip();

                    player.Board.Ships[i++] = _ship;
                }

                _outputManager.WriteLine($"Ships for {player.Name} placed - completed!", ConsoleColor.Yellow);
            }
            catch(Exception e)
            {
                _log.Error($"BTG - Error when trying to place the ships on board: {e}");
            }
        }

        public IShip PlaceShip()
        {
            switch (_shipDirection)
            {
                case ShipDirection.Down:
                    return PlaceShipDown();
                case ShipDirection.Right:
                    return PlaceShipRight();
                case ShipDirection.Left:
                    return PlaceShipLeft();
                default:
                    return PlaceShipUp();
            }
        }
        private IShip PlaceShipUp()
        {
            var positionIndex = 0;
            var maxY = _coordinate.YCoordinate + _ship.Coordinates.Length;

            for (var i = _coordinate.YCoordinate; i < maxY; i++)
            {
                var currentCoordinate = new Coordinate(_coordinate.XCoordinate, i);
                
                _ship.Coordinates[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            return _ship;
        }
        private IShip PlaceShipDown()
        {
            var positionIndex = 0;
            var minY = _coordinate.YCoordinate - _ship.Coordinates.Length;

            for (var i = _coordinate.YCoordinate; i > minY; i--)
            {
                var currentCoordinate = new Coordinate(_coordinate.XCoordinate, i);

                _ship.Coordinates[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            return _ship;
        }

        private IShip PlaceShipRight()
        {
            var positionIndex = 0;
            var maxX = _coordinate.XCoordinate + _ship.Coordinates.Length;

            for (var i = _coordinate.XCoordinate; i < maxX; i++)
            {
                var currentCoordinate = new Coordinate(i, _coordinate.YCoordinate);

                _ship.Coordinates[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            return _ship;
        }
        private IShip PlaceShipLeft()
        {
            var positionIndex = 0;
            var minX = _coordinate.XCoordinate - _ship.Coordinates.Length;

            for (var i = _coordinate.XCoordinate; i > minX; i--)
            {
                var currentCoordinate = new Coordinate(i, _coordinate.YCoordinate);

                _ship.Coordinates[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            return _ship;
        }

        public void CheckShipsForHit(ICoordinate coordinate, IFireShotResponse response, IPlayer player)
        {
            try
            {
                _log.Info("BTG - Check ship for hits!");

                response.ShotStatus = ShotStatus.Miss;

                foreach (var ship in player.Board.Ships)
                {

                    _ship = ship;

                    if (ship.IsSunk)
                        continue;

                    var status = FireAtShip(coordinate);

                    switch (status)
                    {
                        case ShotStatus.HitAndSunk:
                            response.ShotStatus = ShotStatus.HitAndSunk;
                            response.ShipImpactedName = ship.ShipName;

                            player.Board.ShotHistoryList.Add(coordinate, ShotHistory.Hit);
                            break;
                        case ShotStatus.Hit:
                            response.ShotStatus = ShotStatus.Hit;
                            response.ShipImpactedName = ship.ShipName;
                            player.Board.ShotHistoryList.Add(coordinate, ShotHistory.Hit);
                            break;
                    }

                    if (status != ShotStatus.Miss)
                        break;
                }

                if (response.ShotStatus == ShotStatus.Miss)
                {
                    player.Board.ShotHistoryList.Add(coordinate, ShotHistory.Miss);
                }
            }
            catch (Exception e)
            {
                _log.Error($"BTG - Check ship for hits: {e}");
            }
        }

        public ShotStatus FireAtShip(ICoordinate position)
        {
            if (_ship.Coordinates.Contains(position))
            {
                _ship.LifeRemaining--;

                if (_ship.LifeRemaining == 0)
                    return ShotStatus.HitAndSunk;

                return ShotStatus.Hit;
            }

            return ShotStatus.Miss;
        }
    }
}
