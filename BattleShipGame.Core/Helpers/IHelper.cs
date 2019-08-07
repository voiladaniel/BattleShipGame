namespace BattleShipGame.Core.Helpers
{
    public interface IHelper
    {
        string GetLetterFromNumber(int number);

        int GetNumberFromLetter(string letter);
    }
}