using GoMemory.DataAccess;
using GoMemory.Enums;
using GoMemory.Helpers;
using GoMemory.Interfaces;
using GoMemory.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GoMemory.ViewModels
{
    public class WhatYouSeeGamePlayViewModel : GameViewModel
    {
        private int CorrectSelections;
        public Image[] AllImages { get; set; }
        public Image[] ToMatchImages { get; set; }
        public IList<Image> SelectedImages { get; set; }


        public WhatYouSeeGamePlayViewModel(Difficulty difficulty, ResumeModel resume)
             : base(GameType.Guess, difficulty, resume)
        {
         GameModel = new UnorderedGame();
            SetGameImages();
            IsResume();
            NextRound();
        }

     

        /// <summary>
        /// Retrieve collection of image
        /// amount depends on difficulty setting
        /// </summary>
        public void SetGameImages()
        {
            AllImages = ImageHelper.GetImages(DifficultySetting.MaxSelectable);
  }


        /// <summary>
        /// Determines if max level for difficulty is reached if not
        /// next round is initialized
        /// </summary>
        public bool NextRound()
        {

            GameModel.Level += 1;
            if (GameModel.Level <= DifficultySetting.MaxLevel)
            {
                ResumeModel.Level = GameModel.Level - 1;
                //todo pull directly from App.DifficultSettings
                ResumeModel.MatchesNeeded = GameModel.MatchesNeeded;
                ResumeHelper.SetResume(ResumeModel);
                InitializeRound();
                return true;
            }

            ResumeHelper.RemoveResume(ResumeModel.GameType);
            return false;
        }


        /// <summary>
        /// InitializeRound settings
        /// </summary>
        public void InitializeRound()
        {
            CorrectSelections = 0;
            GameModel.MatchesNeeded += 1;
            ToMatchImages = ImageHelper.ToMatchImagesList(GameModel.MatchesNeeded, AllImages);
            SelectedImages = new List<Image>();
        }


        /// <summary>
        /// Create a new GameGrid
        /// </summary>
        /// <returns></returns>
        public Grid CreateNewGrid(Grid grid)
        {
            grid = GridHelper.CreateGrid(DifficultySetting.GridRowSize, DifficultySetting.GridColumnSize);
            grid = GridHelper.InsertGridImages(grid, AllImages, DifficultySetting);
            return grid;
        }


        /// <summary>
        /// Check if the selected image is contain within 
        /// the list of image that are needed to be matched
        /// </summary>
        /// <param name="selectedImage"></param>
        public bool CheckSelections(Image selectedImage)
        {
            foreach (var image in ToMatchImages)
            {
                if (image.Source == selectedImage.Source)
                {
                    CorrectSelections++;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check to see if the number of correct selection is
        /// equal to the total number of correct guesses needed
        /// </summary>
        /// <returns></returns>
        public bool CheckIsRoundComplete()
        {
            return CorrectSelections == ToMatchImages.Length;

        }



        /// <summary>
        /// Set a Labels text to the current level
        /// </summary>
        /// <returns></returns>
        public string SetLevelText()
        {
            return $"Level : {GameModel.Level}";
        }

        /// <summary>
        /// Set retry values
        /// </summary>
        public void Retry()
        {

            GameModel.Level -= 1;
            GameModel.MatchesNeeded -= 1;

        }

        public FlexLayout CreateSequenceFlexLayout(FlexLayout flexLayout)
        {
            flexLayout.Children.Clear();
            for (int i = 0; i < ToMatchImages.Length; i++)
            {
                Image img = new Image
                {
                    Source = ToMatchImages[i].Source,
                    Margin = new Thickness(2)
                };

                flexLayout.Children.Add(img);
            }

            return flexLayout;
        }
    }
}
