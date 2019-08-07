namespace BattleShipGame.Core.Models
{
    public interface IPlayer
    {
        string Name { get; set; }
        int Wins { get; set; }
        bool IsComputer { get; set; }
        IBoard Board { get; set; }
    }
}