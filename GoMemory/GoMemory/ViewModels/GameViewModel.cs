using GoMemory.DataAccess;
using GoMemory.Enums;
using GoMemory.Helpers;
using GoMemory.Interfaces;
using GoMemory.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoMemory.ViewModels
{
    public class GameViewModel : BaseViewModel 
    {
        public DifficultySetting DifficultySetting { get; set; }
        public ImageHelper ImageHelper { get; set; }
        public ResumeModel ResumeModel { get; set; }
        public int GuessesMade { get; set; } = 0;
        public GameType GameType { get; set; }
        public IGameModel GameModel { get; set; }

        public GameViewModel(GameType gameType,Difficulty difficulty, ResumeModel resume)
        {
            GameType = gameType;
            DifficultySetting = SettingsData.SetCurrentDifficulty(gameType, difficulty);
            ImageHelper = new ImageHelper();
          
        }

        public void IsResume()
        {
            if (ResumeModel != null)
            {
              GameModel.Level = ResumeModel.Level;
              GameModel.MatchesNeeded = ResumeModel.MatchesNeeded;
             }
            else
            {
                ResumeModel = new ResumeModel
                {
                    GameType = GameType,
                    Difficulty = DifficultySetting.Difficulty,
                };
            }

        }

        /// <summary>
        /// Set retry values
        /// </summary>
        public void Retry()
        {
            GameModel.Level -= 1;
            GameModel.MatchesNeeded -= 1;


        }


        /// <summary>
        /// Set a Labels text to the current level
        /// </summary>
        /// <returns></returns>
        public string SetLevelText()
        {
            return $"Level : {GameModel.Level}";
        }
    }
}
