
namespace BattleShipGame.BusinessLogic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Core.Config;
    using Core.enums;
    using BattleShipGame.Core.Helpers;
    using Core.Models;
    using Data.Models;
    using BattleShipGame.Core.Managers;

    public class OutputManager : IOutputManager
    {
        private readonly IHelper _helper;

        private ShotHistory _shotHistory;

        public OutputManager(IHelper helper)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
        }
        public void ShowFlashScreen()
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("        ********************************************");
            Console.Write("        *"); Console.ForegroundColor = ConsoleColor.White; Console.Write("    Welcome to The Battleship Game!!!     "); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("*");
            Console.WriteLine("        ********************************************");
            Console.ForegroundColor = ConsoleColor.White;

            var timer = new Timer(ClearFlashScreen, null, 2000, 1000);
            Thread.Sleep(2100);
            timer.Dispose();
        }

        public void ShowHeader()
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("        ************************************");
            Console.Write("        *"); Console.ForegroundColor = ConsoleColor.White;
            Console.Write("       THE BATTLESHIP GAME!!!     "); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("*");
            Console.WriteLine("        ************************************");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void ShowStats(IGameConfig gameConfig)
        {
            Console.Clear();
            ShowHeader();

            var stats = "Player: " + gameConfig.Player.Name + "(Win: " + gameConfig.Player.Wins + ")" +
                         "\t Computer: " + gameConfig.Computer.Name + "(win: " + gameConfig.Computer.Wins + ")";
            Console.WriteLine(stats);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }
        public void WriteLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        private void ClearFlashScreen(Object state)
        {
            Console.Clear();
        }

        public void ShowWhoseTurn(IPlayer player)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(player.Name + " turn... ");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void DrawHistory(IPlayer player)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("  ");
            for (var y = 1; y <= 10; y++)
            {
                Console.Write(y);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;

            List<ShotHistory> list = new List<ShotHistory>();
            for (var x = 1; x <= 10; x++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(_helper.GetLetterFromNumber(x) + " ");
                Console.ForegroundColor = ConsoleColor.White;
                for (int y = 1; y <= 10; y++)
                {
                    _shotHistory = player.Board.CheckCoordinate(new Coordinate(x, y));
                    list.Add(_shotHistory);
                    switch (_shotHistory)
                    {
                        case ShotHistory.PlayerShip:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("S");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        case ShotHistory.Hit:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("H");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case ShotHistory.Miss:
                            Console.Write("M");
                            break;
                       default:
                            Console.Write(" ");
                            break;
                    }
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            Console.WriteLine("");
        }
        public void ShowShotResult(IFireShotResponse shotResponse, ICoordinate coordinate, IPlayer player)
        {
            var str = "";
            switch (shotResponse.ShotStatus)
            {
                case ShotStatus.Duplicate:
                    Console.ForegroundColor = ConsoleColor.Red;
                    str = "Shot location: " + _helper.GetLetterFromNumber(coordinate.XCoordinate) + coordinate.YCoordinate.ToString() + "\t result: Duplicate shot location!";
                    break;
                case ShotStatus.Hit:
                    Console.ForegroundColor = ConsoleColor.Green;
                    str = "Shot location: " + _helper.GetLetterFromNumber(coordinate.XCoordinate) + coordinate.YCoordinate.ToString() + "\t result: Hit!";
                    break;
                case ShotStatus.HitAndSunk:
                    Console.ForegroundColor = ConsoleColor.Green;
                    str = "Shot location: " + _helper.GetLetterFromNumber(coordinate.XCoordinate) + coordinate.YCoordinate.ToString() + "\t result: Hit and Sunk, " + shotResponse.ShipImpactedName + "!";
                    break;
                case ShotStatus.Invalid:
                    Console.ForegroundColor = ConsoleColor.Red;
                    str = "Shot location: " + _helper.GetLetterFromNumber(coordinate.XCoordinate) + coordinate.YCoordinate.ToString() + "\t result: Invalid hit location!";
                    break;
                case ShotStatus.Miss:
                    Console.ForegroundColor = ConsoleColor.White;
                    str = "Shot location: " + _helper.GetLetterFromNumber(coordinate.XCoordinate) + coordinate.YCoordinate.ToString() + "\t result: Miss!";
                    break;
                case ShotStatus.Victory:
                    Console.ForegroundColor = ConsoleColor.Green;
                    str = "Shot location: " + _helper.GetLetterFromNumber(coordinate.XCoordinate) + coordinate.YCoordinate.ToString() + "\t result: Hit and Sunk, " + shotResponse.ShipImpactedName + "! \n\n";
                    str += "       ******\n";
                    str += "       ******\n";
                    str += "        **** \n";
                    str += "         **  \n";
                    str += "         **  \n";
                    str += "       ******\n";
                    str += "Game Over, " + player.Name + " wins!";
                    break;
            }
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }
        public void ShowInvalidInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var str = "Result: Invalid hit location!";
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
