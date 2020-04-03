using GoMemory.Enums;
using GoMemory.Interfaces;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GoMemory.Models
{
    public class ComplexColorGame : IGameModel
    {
       public int Level { get; set; }
        public int MatchesNeeded { get; set; } = 1;
  }
}
