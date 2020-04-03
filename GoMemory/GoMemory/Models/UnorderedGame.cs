using GoMemory.Interfaces;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GoMemory.Models
{
    public class UnorderedGame : IGameModel
    {
     
        public int Level { get; set; } = 0;
        public int MatchesNeeded { get; set ; } = 2;
    }
}
