using GoMemory.Interfaces;
using Xamarin.Forms;

namespace GoMemory.Models
{
    public class OrderedGame : IGameModel
    {
       public int Level { get; set; }
        public int MatchesNeeded { get; set; } = 2;
    }
}
