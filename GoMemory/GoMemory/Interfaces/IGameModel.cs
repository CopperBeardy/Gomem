using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GoMemory.Interfaces
{
    public interface IGameModel
    {
        public int Level { get; set; }

        public int MatchesNeeded { get; set; }
    }
}
