using GoMemory.Enums;
using GoMemory.Models;
using System;
using System.Collections.Generic;

namespace GoMemory.DataAccess
{
    public static class SettingsData
    {

        public static List<DifficultySetting> CreateDifficultySettings()
        {
            List<DifficultySetting> DifficultySettings = new List<DifficultySetting>();

            DifficultySettings.Add(new DifficultySetting
            {
                GameType = GameType.Guess,
                Difficulty = Difficulty.Easy,
                GridRowSize = 4,
                GridColumnSize = 4,
                MaxSelectable = 16,
                MaxLevel = 10
            });

            DifficultySettings.Add(new DifficultySetting
            {
                GameType = GameType.Guess,
                Difficulty = Difficulty.Normal,
                GridRowSize = 5,
                GridColumnSize = 5,
                MaxSelectable = 25,
                MaxLevel = 20
            });

            DifficultySettings.Add(new DifficultySetting
            {
                GameType = GameType.Guess,
                Difficulty = Difficulty.Hard,
                GridRowSize = 6,
                GridColumnSize = 6,
                MaxSelectable = 36,
                MaxLevel = 30
            });

            DifficultySettings.Add(new DifficultySetting
            {
                GameType = GameType.Sequential,
                Difficulty = Difficulty.Easy,
                GridRowSize = 2,
                GridColumnSize = 2,
                MaxSelectable = 4,
                MaxLevel = 10
            });

            DifficultySettings.Add(new DifficultySetting
            {
                GameType = GameType.Sequential,
                Difficulty = Difficulty.Normal,
                GridRowSize = 3,
                GridColumnSize = 3,
                MaxSelectable = 9,
                MaxLevel = 20
            });

            DifficultySettings.Add(new DifficultySetting
            {
                GameType = GameType.Sequential,
                Difficulty = Difficulty.Hard,
                GridRowSize = 4,
                GridColumnSize = 4,
                MaxSelectable = 16,
                MaxLevel = 30
            });

            DifficultySettings.Add(new DifficultySetting
            {
                GameType = GameType.ColourComplex,
                Difficulty = Difficulty.Easy,
                GridRowSize = 3,
                GridColumnSize = 1,
                MaxSelectable = 3,
                MaxLevel = 10
            });

            DifficultySettings.Add(new DifficultySetting
            {
                GameType = GameType.ColourComplex,
                Difficulty = Difficulty.Normal,
                GridRowSize = 3,
                GridColumnSize = 2,
                MaxSelectable = 6,
                MaxLevel = 20
            });

            DifficultySettings.Add(new DifficultySetting
            {
                GameType = GameType.ColourComplex,
                Difficulty = Difficulty.Hard,
                GridRowSize = 3,
                GridColumnSize = 3,
                MaxSelectable = 9,
                MaxLevel = 32
            });

            return DifficultySettings;
        }

        public static DifficultySetting SetCurrentDifficulty(GameType gameType, Difficulty difficulty)
        {
            return CreateDifficultySettings()
                           .Find(x => x.GameType.Equals(gameType) &&
                               x.Difficulty.Equals(difficulty));
        }
    }
}
