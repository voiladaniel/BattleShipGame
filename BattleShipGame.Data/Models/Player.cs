
namespace BattleShipGame.Data.Models
{
    using BattleShipGame.Core.Models;

    public class Player : IPlayer
    {
        public string Name { get; set; }
        public int Wins { get; set; }
        public bool IsComputer { get; set; }
        public IBoard Board { get; set; }
    }
}
