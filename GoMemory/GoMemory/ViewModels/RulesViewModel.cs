using GoMemory.Enums;

namespace GoMemory.ViewModels
{
    public class RulesViewModel
    {
        public GameType GameType { get; set; }
        public RulesViewModel(GameType gameType)
        {
            GameType = gameType;
        }
    }
}
