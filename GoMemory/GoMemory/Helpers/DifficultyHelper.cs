using GoMemory.Models;

namespace GoMemory.Helpers
{
    public static class DifficultyHelper
    {
        public static DifficultySetting SetDifficulty(int columSize, int rowSize, int maxSelectable, int maxLevel)
        {
            return new DifficultySetting()
            {
                GridColumnSize = columSize,
                GridRowSize = rowSize,
                MaxSelectable = maxSelectable,
                MaxLevel = maxLevel
            };
        }
    }
}
