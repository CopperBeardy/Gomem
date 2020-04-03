using GoMemory.Enums;

namespace GoMemory.Interfaces
{
    public interface IGame
    {
        bool NextRound();
        void InitializeRound();
        string SetLevelText();
        void Retry();
    }
}
