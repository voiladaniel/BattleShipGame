
namespace BattleShipGame.BusinessLogic.Managers
{
    using System;
    using BattleShipGame.Core.Helpers;
    using BattleShipGame.Core.Managers;
    using Core.Models;
    using Data.Models;
    using log4net;

    public class InputManager : IInputManager
    {
        private readonly IOutputManager _outputManager;

        private readonly IPlayer _player;

        private readonly IHelper _helper;

        private readonly ILog _log;

        private string _inputResult;

        private int _xCoordinate;

        private int _yCoordinate;

        public InputManager(
            IOutputManager outputManager,
            IPlayer player,
            IHelper helper,
            ILog log)
        {
            _outputManager = outputManager ?? throw new ArgumentNullException(nameof(outputManager));
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
            _log = log ?? throw new ArgumentException(nameof(log));
        }
        public string GetUserName()
        {
            try
            {
                _log.Info("BTG - Get the name from input.");
                do
                {
                    _outputManager.Write("Input player Name: ");
                    _player.Name = Read();
                }
                while (string.IsNullOrEmpty(_player.Name.Trim()));
                _log.Info("BTG - Get the name - Completed!");
            }
            catch (Exception e)
            {
                _log.Error($"BTG - Error when trying to get the name :{e}");
            }

            return _player.Name;
        }

        public string Read()
        {
            try
            {
                _log.Info("BTG - Get the input.");

                var message = Console.ReadLine();

                _log.Info("BTG - Get the input - Completed!");
                return message;
            }
            catch (Exception e)
            {
                _log.Error($"BTG - Error when trying to get the input :{e}");
            }

            return null;
        }
        public bool CheckQuit()
        {
            Console.WriteLine("Press F5 to replay or any key to quit...");
            return Console.ReadKey().Key == ConsoleKey.F5;
        }
        public ICoordinate GetShotLocationManually()
        {
            try
            {
                while (true)
                {
                    _log.Info("BTG - Get the shot location manually!");

                    Console.Write("Which location do you want to shot? ");
                    _inputResult = Console.ReadLine();
                    if (_inputResult.Trim().Length > 1)
                    {
                        _xCoordinate = _helper.GetNumberFromLetter(_inputResult.Substring(0, 1));
                        if (_xCoordinate > 0 && int.TryParse(_inputResult.Substring(1), out _yCoordinate))
                        {
                            return new Coordinate(_xCoordinate, _yCoordinate);
                        }

                        _outputManager.ShowInvalidInput();
                    }
                }
            }
            catch(Exception e)
            {
                _log.Error($"BTG - Error when trying to get the shot location from input: {e}");
            }

            return null;
        }
    }
}
