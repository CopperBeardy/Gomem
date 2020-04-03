using GoMemory.DataAccess;
using GoMemory.Enums;
using GoMemory.Helpers;
using GoMemory.Interfaces;
using GoMemory.Models;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GoMemory.ViewModels
{
    class ColorComplexGamePlayViewModel : GameViewModel, IGame
    {
        public ComplexColor[] Colors { get; set; }
        public ComplexColor[] PlayColors { get; set; }
        public ComplexColor[] SequenceColors { get; set; }
        public ComplexColor[] SelectedItems { get; set; }
        public Mode Mode { get; set; }

        public ColorComplexGamePlayViewModel(Difficulty difficulty, ResumeModel resume)
                 : base(GameType.ColourComplex, difficulty, resume)
        {
            GameModel = new ComplexColorGame();
            Colors = ColorsCollection.ColorsArray();
            PlayColors = new ComplexColor[DifficultySetting.MaxSelectable];
            for (int i = 0; i < DifficultySetting.MaxSelectable; i++)
            {
                PlayColors[i] = Colors[i];
            }
            IsResume();
        }

        

        /// <summary>
        /// Return a string to set the level
        /// </summary>
        /// <returns></returns>
        public string SetLevelText()
        {
            return $"Level : {GameModel.Level}";
        }

        /// <summary>
        /// set values ready to generate the next round values
        /// </summary>
        public void Retry()
        {
           GameModel.Level -= 1;
            GameModel.MatchesNeeded -= 1;
        }


        /// <summary>
        /// Determiners if max level for difficulty is reached if not
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
            GameModel.MatchesNeeded += 1;
            GuessesMade = 0;
            SequenceColors = new ComplexColor[GameModel.MatchesNeeded];
            GenerateRoundToMatchComplexColors();
            SetMode();
        }


        /// <summary>
        /// Populate the stack layout with the sequence of Complex colors
        /// </summary>
        /// <param name="stackLayout"></param>
        /// <returns></returns>
        public StackLayout PopulateSequenceStackLayout(StackLayout stackLayout)
        {
            for (int i = 0; i < SequenceColors.Length; i++)
            {
                StackLayout st = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };
                Label itemnumber = new Label
                {
                    Text = $"{(i + 1)} . "
                };

                Label label = new Label
                {
                    Text = SequenceColors[i].SpeltColor,
                    TextColor = SequenceColors[i].TextColor,


                    FontSize = 25,
                    FontAttributes = FontAttributes.Bold,
                    Margin = new Thickness(0, 2, 0, 0)
                };

                st.Children.Add(itemnumber);
                st.Children.Add(label);
                stackLayout.Children.Add(st);
            }

            return stackLayout;
        }

        /// <summary>
        ///  set the color and text that will be need for the 
        /// round
        /// </summary>
        public void GenerateRoundToMatchComplexColors()
        {
            Random rnd = new Random();

            for (int i = 0; i < SequenceColors.Length; i++)
            {
                int colourRndIndex = rnd.Next(0, PlayColors.Length);
                int textRndIndex = rnd.Next(0, PlayColors.Length);
              SequenceColors[i] = new ComplexColor
                {                  
                    SpeltColor = PlayColors[colourRndIndex].SpeltColor,
                    TextColor = PlayColors[textRndIndex].TextColor,
                };               

            }
        }


        /// <summary>
        /// randomly set the round mode
        /// </summary>
        public string SetMode()
        {
            Random rnd = new Random();
            int num = rnd.Next(0, 2);

            Mode = num == 1 ? Mode.Color : Mode.Text;
            SelectedItems = new ComplexColor[GameModel.MatchesNeeded];
             
            if (Mode == Mode.Text)
            {
                 return "Mode: How its spelt ?";
            }          
            return "Mode: How was it colored ?";

        }


        /// <summary>
        /// Check if the selected button text match the correct 
        /// mode  colour
        /// must be in correct order
        /// </summary>
        /// <param name="colourSelected"></param>
        public bool CheckSequenceText(string colourSelected)
        {
            ComplexColor guessColor = new ComplexColor
            {
                SpeltColor = colourSelected
            };
            SelectedItems[GuessesMade] = guessColor;

            for (int i = 0; i < SelectedItems.Length; i++)
            {
                if (SelectedItems[i] == null)
                    break;
                
                if(!SelectedItems[i].SpeltColor.Equals(
                    SequenceColors[i].SpeltColor) )
                    return false;
            }

            GuessesMade += 1;
            return true;
        }
        /// <summary>
        /// Check if the selected button text match the correct 
        /// mode  colour
        /// must be in correct order
        /// </summary>
        /// <param name="textColour"></param>
        public bool CheckSequenceColour(Color textColour)
        {
            //var color = ColorConverters.FromHex(textColour.ToHex());
            ComplexColor guessColor = new ComplexColor
            {
                TextColor = textColour
            }; 
         
            SelectedItems[GuessesMade] = guessColor;

            for (int i = 0; i < SelectedItems.Length; i++)
            {
                if (SelectedItems[i] == null)
                    break;
                if ((Color)SelectedItems[i].TextColor !=
                       SequenceColors[i].TextColor)
                    return false;
            }
            GuessesMade += 1;
            return true;
        }

        /// <summary>
        /// Create the grid for the Guessing buttons
        /// </summary>
        /// <returns></returns>
        public Grid GenerateGrid()
        {
            Grid grid = GridHelper.CreateGrid(DifficultySetting.GridRowSize, DifficultySetting.GridColumnSize);
            grid = AddSelectionButtonsToGrid(grid);

            return grid;
        }


        /// <summary>
        /// add the guess buttons to the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private Grid AddSelectionButtonsToGrid(Grid grid)
        {
            int index = 0;
            try
            {
                for (int row = 0; row < DifficultySetting.GridRowSize; row++)
                {
                    for (int column = 0; column < DifficultySetting.GridColumnSize; column++)
                    {
                        Button button = new Button();

                        button.Text = PlayColors[index].SpeltColor;
                        button.TextColor = PlayColors[index].TextColor;
                        button.BorderColor = Color.Black;
                        button.BackgroundColor = Color.White;     
                        button.Margin = 3;
                        button.FontAttributes = FontAttributes.Bold;

                       

                     

                        grid.Children.Add(button, row, column);

                        index += 1;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(AddSelectionButtonsToGrid), ex);
            }

            return grid;
        }


        public bool CheckIsRoundComplete()
        {
            return GuessesMade == GameModel.MatchesNeeded;
        }
    }
}
