
namespace BattleShipGame.BusinessLogic.Managers
{
    using System;
    using Core.Config;
    using Core.enums;
    using BattleShipGame.Core.Helpers;
    using BattleShipGame.Core.Managers;
    using Core.Models;
    using Data.Models;
    using log4net;

    public class SetupManager : ISetupManager
    {
        private readonly ILog _log ;

        private readonly IOutputManager _outputManager;

        private readonly IInputManager _inputManager;

        private readonly IGameConfig _gameConfig;

        private readonly IRandomManager _randomManager;

        private readonly IShipManager _shipManager;

        private ICoordinate _whereToShot;

        private ICoordinate _shotPoint;

        private IFireShotResponse _fireShotResponse;

        private readonly ICoordinateManager _coordinateManager;

        private readonly IShotManager _shotManager;

        public SetupManager(
            IOutputManager outputManager, 
            IInputManager inputManager, 
            IGameConfig gameConfig, 
            IRandomManager randomManager,
            IShipManager shipManager,
            ICoordinateManager coordinateManager,
            IShotManager shotManager,
            ILog log)
        {
            _outputManager = outputManager ?? throw new ArgumentNullException(nameof(outputManager));
            _inputManager = inputManager ?? throw new ArgumentNullException(nameof(outputManager));
            _gameConfig = gameConfig ?? throw new ArgumentNullException(nameof(gameConfig));
            _randomManager = randomManager ?? throw new ArgumentNullException(nameof(gameConfig));
            _shipManager = shipManager ?? throw new ArgumentNullException(nameof(shipManager));
            _coordinateManager = coordinateManager ?? throw new ArgumentNullException(nameof(coordinateManager));
            _shotManager = shotManager ?? throw new ArgumentNullException(nameof(shotManager));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }
        public void Start()
        {
            try
            {
                _log.Info("BTG - Initialization Started.");

                _outputManager.ShowFlashScreen();
                _outputManager.ShowHeader();

                _gameConfig.Player.Name = _inputManager.GetUserName();
                _gameConfig.Player.IsComputer = false;
                _gameConfig.Computer.Name = "ZORRO";
                _gameConfig.Computer.IsComputer = true;

                _outputManager.ShowStats(_gameConfig);
                _gameConfig.IsPlayerTurn = _randomManager.WhoseFirst();

                _log.Info("BTG - Initialization Completed.");
            }
            catch (Exception e)
            {
                _log.Error($"BTG - Initialization Error {e}.");
            }

            do
            {
                SetBoard();

                do
                {
                    try
                    {
                        _log.Info("BTG - Game Flow Started!");
                        _outputManager.ShowWhoseTurn(_gameConfig.IsPlayerTurn ? _gameConfig.Player : _gameConfig.Computer);
                        _outputManager.DrawHistory(_gameConfig.IsPlayerTurn ? _gameConfig.Computer : _gameConfig.Player);

                        _fireShotResponse = Shot(_gameConfig.IsPlayerTurn ? _gameConfig.Computer : _gameConfig.Player,
                            _gameConfig.IsPlayerTurn ? _gameConfig.Player : _gameConfig.Computer,
                            out _shotPoint);

                        _outputManager.Clear();
                        _outputManager.ShowHeader();
                        _outputManager.ShowStats(_gameConfig);
                        _outputManager.ShowWhoseTurn(_gameConfig.IsPlayerTurn ? _gameConfig.Player : _gameConfig.Computer);
                        _outputManager.DrawHistory(_gameConfig.IsPlayerTurn ? _gameConfig.Computer : _gameConfig.Player);
                        _outputManager.ShowShotResult(_fireShotResponse, _shotPoint, _gameConfig.IsPlayerTurn ? _gameConfig.Player : _gameConfig.Computer);

                        if (_fireShotResponse.ShotStatus != ShotStatus.Victory)
                        {
                            _outputManager.WriteLine("Press any key to continue to switch to " + (_gameConfig.IsPlayerTurn
                                                         ? _gameConfig.Computer.Name
                                                         : _gameConfig.Player.Name), ConsoleColor.Cyan);

                            _gameConfig.IsPlayerTurn = !_gameConfig.IsPlayerTurn;
                            _inputManager.Read();

                            _outputManager.Clear();
                            _outputManager.ShowHeader();
                            _outputManager.ShowStats(_gameConfig);
                        }
                    }
                    catch (Exception e)
                    {
                        _log.Error($"BTG - Game flow error: {e}");
                    }
                    
                } while (_fireShotResponse.ShotStatus != ShotStatus.Victory);
            } while (!_inputManager.CheckQuit());

            _log.Info("BTG - Exit Game.");
        }

        public void SetBoard()
        {
            try
            {
                _log.Info("BTG - Setting up boards!");

                _gameConfig.Player.Board = new Board();
                _shipManager.PlaceShipsOnBoard(_gameConfig.Player);

                _gameConfig.Computer.Board = new Board();
                _shipManager.PlaceShipsOnBoard(_gameConfig.Computer);

                _log.Info("BTG - Setting up boards completed!");
            }
            catch(Exception e)
            {
                _log.Error($"BTG - Setting up boards error: {e}");
            }
           
        }

        private IFireShotResponse Shot(IPlayer victim, IPlayer shooter, out ICoordinate shotPoint)
        {
            IFireShotResponse fireShotResponse;
            try
            {
                _log.Info("BTG - Trying to shot!");

                do
                {
                    if (!shooter.IsComputer)
                    {
                        _whereToShot = _inputManager.GetShotLocationManually();
                        fireShotResponse = _shotManager.FireShot(victim, _whereToShot);
                        if (fireShotResponse.ShotStatus == ShotStatus.Invalid ||
                            fireShotResponse.ShotStatus == ShotStatus.Duplicate)
                            _outputManager.ShowShotResult(fireShotResponse, _whereToShot, shooter);
                    }
                    else
                    {
                        _whereToShot = _coordinateManager.GetRandomCoordinate();
                        ;
                        fireShotResponse = _shotManager.FireShot(victim, _whereToShot);
                    }

                    if (fireShotResponse.ShotStatus == ShotStatus.Victory)
                    {
                        shooter.Wins += 1;
                    }
                } while (fireShotResponse.ShotStatus == ShotStatus.Duplicate ||
                         fireShotResponse.ShotStatus == ShotStatus.Invalid);

                shotPoint = _whereToShot;

                _log.Info("BTG - Shot completed!");

                return fireShotResponse;
            }
            catch(Exception e)
            {
                _log.Error($"BTG - Error when tryinh to shot: {e}");
            }

            shotPoint = null;
            return null;
        }
    }
}
