
namespace BattleShipGame.BusinessLogic.Managers
{
    using Core.enums;
    using BattleShipGame.Core.Helpers;
    using BattleShipGame.Core.Managers;
    using Core.Models;
    using System;
    using log4net;


    public class ShotManager : IShotManager
    {
        
        private readonly IFireShotResponse _fireShotResponse;

        private readonly IShipManager _shipManager;

        private readonly IBoard _board;

        private readonly ILog _log;

        public ShotManager(
            IShipManager shipManager,
            IFireShotResponse fireShotResponse,
            IBoard board,
            ILog log)
        {
            _shipManager = shipManager ?? throw new ArgumentNullException(nameof(shipManager));
            _fireShotResponse = fireShotResponse ?? throw new ArgumentNullException(nameof(fireShotResponse));
            _board = board ?? throw new ArgumentNullException(nameof(board));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public IFireShotResponse FireShot(IPlayer player, ICoordinate coordinate)
        {
            try
            {
                _log.Info("BTG - Check coordinates if exist on board already!");

                if(!_board.IsCoordinateOnBoard(coordinate))
                {
                    _fireShotResponse.ShotStatus = ShotStatus.Invalid;
                    return _fireShotResponse;
                }
            }
            catch (Exception e)
            {
                _log.Error($"BTG - {e}");
            }

            try
            {
                _log.Info("BTG - check if already shoot in that place");

                if (player.Board.ShotHistoryList.ContainsKey(coordinate))
                {
                    _fireShotResponse.ShotStatus = ShotStatus.Duplicate;
                    return _fireShotResponse;
                }
            }
            catch (Exception e)
            {
                _log.Error($"BTG - check if already shoted in that place error: {e}");
            }
            
            try
            {
                _log.Info("BTG - Check for hit and victory!");

                _shipManager.CheckShipsForHit(coordinate, _fireShotResponse, player);
                _board.CheckForVictory(_fireShotResponse, player);
            }
            catch (Exception e)
            {
                _log.Error($"BTG - check for hit and victory error: {e}");
            }

            return _fireShotResponse;
        }
    }
}
