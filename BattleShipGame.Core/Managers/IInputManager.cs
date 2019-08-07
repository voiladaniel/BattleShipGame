
namespace BattleShipGame.Core.Managers
{
    using Models;

    public interface IInputManager
    {
        string GetUserName();

        bool CheckQuit();

        ICoordinate GetShotLocationManually();
        string Read();
    }
}