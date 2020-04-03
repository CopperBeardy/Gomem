using GoMemory.DataAccess;
using GoMemory.Enums;
using GoMemory.Helpers;
using GoMemory.Interfaces;
using GoMemory.Models;
using System;
using Xamarin.Forms;

namespace GoMemory.ViewModels
{
    public class SequentialGamePlayViewModel : GameViewModel
    {

        public Image[] AllImages { get; set; }
        public Image[] ToMatchImages { get; set; }
        public Image[] SelectedImages { get; set; }


        public SequentialGamePlayViewModel(Difficulty difficulty, ResumeModel resume) 
            :base (GameType.Sequential,difficulty, resume)
        {

             GameModel = new OrderedGame();
            SetGameImages();
            IsResume();
        
        }

     

        /// <summary>
        /// Retrieve the image need for the selection Grid and for
        /// generate sequences
        /// </summary>
        public void SetGameImages()
        {
            AllImages = ImageHelper.GetImages(DifficultySetting.MaxSelectable);
           
        }

        /// <summary>
        /// Adds images for the round to the sequence layout
        /// </summary>
        /// <param name="layout"></param>
        /// <returns></returns>
        public StackLayout PopulateSequenceStackLayout(StackLayout layout)
        {
            for (int i = 0; i < ToMatchImages.Length; i++)
            {
                StackLayout st = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,


                };
                Label itemnumber = new Label
                {
                    Text = $"{(i + 1)} . "
                };
                Image img = new Image
                {
                    Source = ToMatchImages[i].Source,

                    Margin = new Thickness(2)
                };
                st.Children.Add(itemnumber);
                st.Children.Add(img);
                layout.Children.Add(st);

            }

            return layout;
        }


        /// <summary>
        /// Create a grid containing the image used at this level of difficulty
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public Grid CreateNewGrid(Grid grid)
        {
            grid = GridHelper.CreateGrid(DifficultySetting.GridRowSize, DifficultySetting.GridColumnSize);
            grid = AddGridImages(grid);


            return grid;
        }

        public Grid AddGridImages(Grid grid)
        {
            return GridHelper.InsertGridImages(grid, AllImages, DifficultySetting);

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
                ResumeModel.MatchesNeeded = GameModel.MatchesNeeded;
                ResumeHelper.SetResume(ResumeModel);
                GameModel.MatchesNeeded += 1;
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
            ToMatchImages = new Image[GameModel.MatchesNeeded];
            SelectedImages = new Image[GameModel.MatchesNeeded];

            GenerateToMatchSequence();
            GuessesMade = 0;
        }




        /// <summary>
        /// Generate the sequence that needs to be matched can have multiple images of the same type
        /// </summary>
        private void GenerateToMatchSequence()
        {

            Random rnd = new Random();
            for (int i = 0; i < ToMatchImages.Length; i++)
            {

                int randomValue = rnd.Next(0, AllImages.Length);
                Image img = new Image
                {
                    Source = AllImages[randomValue].Source,
                    Aspect = Aspect.Fill,
                    Margin = new Thickness(2)

                };
                ToMatchImages[i] = img;
            }
        }


        /// <summary>
        /// Check if the selected image is contain within 
        /// the array of image that are needed to be matched 
        /// must be in correct order
        /// </summary>
        /// <param name="selectedImage"></param>
        public bool CheckSequence(Image selectedImage)
        {
            SelectedImages[GuessesMade] = selectedImage;

            for (int i = 0; i < GuessesMade + 1; i++)
            {
                if (SelectedImages[i].Source != ToMatchImages[i].Source)
                    return false;
                else
                    continue;
            }

            GuessesMade += 1;
            return true;
        }

        public bool CheckIsRoundComplete()
        {
            return GuessesMade == ToMatchImages.Length;
        }

    }
}
