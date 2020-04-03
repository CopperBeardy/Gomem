using GoMemory.Enums;
using GoMemory.Helpers;
using GoMemory.Models;
using GoMemory.ViewModels;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace GoMemory.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColourComplexGamePlayPage : ContentPage
    {
        readonly ColorComplexGamePlayViewModel _colourComplexGamePlayViewModel;
        private GameStatus GameStatus;

        public ColourComplexGamePlayPage(Difficulty difficulty, GameType gameType, ResumeModel resume)
        {
            InitializeComponent();
            Title = "Colour Complex";
            GameStatus = new GameStatus
            {
                Difficulty = difficulty,
                GameType = gameType
            };

            _colourComplexGamePlayViewModel = new ColorComplexGamePlayViewModel(difficulty, resume);
            GuessLayout();
            NextRound();
        }



        /// <summary>
        /// initiate the visibility of elements and 
        /// changes the page content
        /// </summary>
        public void NextRound()
        {
            bool next;
            next = _colourComplexGamePlayViewModel.NextRound();
            if (next)
            {

                StackLayout.IsVisible = true;
                PlayLayout.IsVisible = false;
                Failed.IsVisible = false;
                SelectedStackLayout.Children.Clear();
                SequenceStackLayout = _colourComplexGamePlayViewModel.PopulateSequenceStackLayout(SequenceStackLayout);
                LevelLabel.Text = _colourComplexGamePlayViewModel.SetLevelText();
                Content = StackLayout;
            }
            else
            {
                StackLayout.IsVisible = false;
                PlayLayout.IsVisible = false;
                DifficultyCompleted();
            }
        }

        /// <summary>
        /// Make visible to difficulty completeImage
        /// </summary>
        private void DifficultyCompleted()
        {
            Complete.IsVisible = true;
        }

        private void GuessLayout()
        {
            Grid = _colourComplexGamePlayViewModel.GenerateGrid();
            WidthRequest = Application.Current.MainPage.Width - 10;
            HeightRequest = Application.Current.MainPage.Height * 0.6;
            Frame.WidthRequest = Application.Current.MainPage.Width * 0.7;
            Frame.HeightRequest = Application.Current.MainPage.Height * 0.1;
            Grid.WidthRequest = Application.Current.MainPage.Width * 0.5;
            Grid.HeightRequest = Application.Current.MainPage.Height * 0.6;

            PlayLayout.Children.Add(Grid);
            AddGuessButtonClickHandlers();

        }
        private void StartButton_OnClicked(object sender, EventArgs e)
        {

            SequenceStackLayout.Children.Clear();
            StackLayout.IsVisible = false;
            PlayLayout.IsVisible = true;
            ModeLabel.Text = _colourComplexGamePlayViewModel.SetMode();
            Content = PlayLayout;

        }

        private void AddGuessButtonClickHandlers()
        {
            foreach (var child in Grid.Children)
            {
                if (child is Button btn) btn.Clicked += OnTapped;
            }
        }


        /// <summary>
        /// Handles the clicking of a button once guessing phase has started
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void OnTapped(object sender, EventArgs ev)
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            bool found = false;
            try
            {
                Grid.IsEnabled = false;
                Button btn = sender as Button;

                if (_colourComplexGamePlayViewModel.Mode == Mode.Text)
                {
                    found = _colourComplexGamePlayViewModel.CheckSequenceText(btn.Text);
                }
                else if (_colourComplexGamePlayViewModel.Mode == Mode.Color)
                {
                    found = _colourComplexGamePlayViewModel.CheckSequenceColour(btn.TextColor);

                }
                if (found)
                {
                    Label label = new Label
                    {
                        Text = btn.Text,
                        TextColor = System.Drawing.Color.FromName(btn.Text),
                        FontSize = 25,
                        FontAttributes = FontAttributes.Bold,
                        Margin = new Thickness(0, 2, 5, 0)
                    };

                    if (SelectedStackLayout.Children.Count > 2)
                    {
                        SelectedStackLayout.Children.RemoveAt(0);
                    }
                    SelectedStackLayout.Children.Add(label);
                }
                else
                {
                    Failed.IsVisible = true;
                    SequenceStackLayout.Children.Clear();
                    Content = Failed;

                }
                if (_colourComplexGamePlayViewModel.CheckIsRoundComplete())
                {

                    GameStatus.Level = _colourComplexGamePlayViewModel.GameModel.Level;
                    UpdateStatusHelper.UpdateStatus(GameStatus);

                    NextRound();
                }


            }
            catch (Exception e)
            {
                //TODO: throw the exception and handle it properly idiot !!
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                Grid.IsEnabled = true;
                IsBusy = false;
            }

        }

        private void RetryButton_Clicked(object sender, EventArgs e)
        {
            _colourComplexGamePlayViewModel.Retry();

            //Todo extract reduce duplication
            StackLayout.IsVisible = true;
            Failed.IsVisible = false;

            PlayLayout.IsVisible = false;
            Content = StackLayout;

            NextRound();
        }
    }
}