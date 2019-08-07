
namespace BattleShipGame.Core.Managers
{
    using System;
    using Config;
    using Helpers;
    using Models;

    public interface IOutputManager
    {
        void ShowFlashScreen();
        void ShowHeader();
        void Write(string message);
        void ShowStats(IGameConfig gameConfig);
        void ShowWhoseTurn(IPlayer player);
        void WriteLine(string message, ConsoleColor color);
        void ShowShotResult(IFireShotResponse shotResponse, ICoordinate coordinate, IPlayer player);
        void DrawHistory(IPlayer player);
        void ShowInvalidInput();
        void Clear();
    }
}