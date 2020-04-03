using GoMemory.Enums;
using GoMemory.Helpers;
using GoMemory.Models;
using GoMemory.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace GoMemory.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SequentialGamePlayPage : ContentPage
    {
        private readonly SequentialGamePlayViewModel _sequentialGamePlayViewModel;
        private GameStatus GameStat;
        public SequentialGamePlayPage(Difficulty difficulty, GameType gameType, ResumeModel resume)
        {
            InitializeComponent();
            Title = "Sequential";
            GameStat = new GameStatus
            {
                Difficulty = difficulty,
                GameType = gameType
            };
            _sequentialGamePlayViewModel = new SequentialGamePlayViewModel(difficulty, resume);

            NextRound();

            GuessLayout();
        }



        /// <summary>
        /// initiate the visibility of elements and 
        /// changes the page content
        /// </summary>
        public void NextRound()
        {
            bool next;
            next = _sequentialGamePlayViewModel.NextRound();
            if (next)
            {

                StackLayout.IsVisible = true;
                PlayLayout.IsVisible = false;
                Failed.IsVisible = false;
                SequenceStackLayout = _sequentialGamePlayViewModel.PopulateSequenceStackLayout(SequenceStackLayout);
                LevelLabel.Text = _sequentialGamePlayViewModel.SetLevelText();
                Content = StackLayout;
            }
            else
            {
                DifficultyCompleted();
            }


        }

        private void StartButton_OnClicked(object sender, EventArgs e)
        {

            SequenceStackLayout.Children.Clear();
            SelectedImageStackLayout.Children.Clear();
            StackLayout.IsVisible = false;
            PlayLayout.IsVisible = true;

            Content = PlayLayout;

        }

        /// <summary>
        /// Make visible to difficult completeImage
        /// </summary>
        private void DifficultyCompleted()
        {
            Complete.IsVisible = true;
        }

        /// <summary>
        /// Create the Grid for making guesses
        /// </summary>
        private void GuessLayout()
        {
            Grid = _sequentialGamePlayViewModel.CreateNewGrid(Grid);

            WidthRequest = Application.Current.MainPage.Width - 10;
            HeightRequest = Application.Current.MainPage.Height * 0.6;
            Frame.WidthRequest = Application.Current.MainPage.Width * 0.7;
            Frame.HeightRequest = Application.Current.MainPage.Height * 0.1;
            Grid.WidthRequest = Application.Current.MainPage.Width * 0.5;
            Grid.HeightRequest = Application.Current.MainPage.Height * 0.6;

            PlayLayout.Children.Add(Grid);
            AddTapGestures();

        }

        /// <summary>
        /// Add Tap gesture for the Grid Images
        /// </summary>
        public void AddTapGestures()
        {
            //Todo refactor to separate class to handle tap gestures
            foreach (var view in Grid.Children)
            {
                Image image = view as Image;
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += OnTapped;
                image.GestureRecognizers.Add(tapGestureRecognizer);
            }

        }

        /// <summary>
        /// Handles the tapping of a image once guessing phase has started
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
            bool found;
            try
            {

                Image img = sender as Image;
                found = _sequentialGamePlayViewModel.CheckSequence(img);
                if (found)
                {
                    Image sImage = new Image { Source = img.Source };

                    if (SelectedImageStackLayout.Children.Count > 2)
                    {
                        SelectedImageStackLayout.Children.RemoveAt(0);
                    }
                    SelectedImageStackLayout.Children.Add(sImage);
                }
                else
                {
                    Failed.IsVisible = true;
                    SequenceStackLayout.Children.Clear();
                    Content = Failed;

                }
                if (_sequentialGamePlayViewModel.CheckIsRoundComplete())
                {
                    GameStat.Level = _sequentialGamePlayViewModel.GameModel.Level;
                    UpdateStatusHelper.UpdateStatus(GameStat);
                    SequenceStackLayout.Children.Clear();
                    NextRound();
                }


            }
            catch (Exception e)
            {
                //todo look at what might fail and how to handle
                Console.WriteLine(e);
                throw;
            }
            finally
            {

                IsBusy = false;
            }

        }

        private void RetryButton_Clicked(object sender, EventArgs e)
        {
            _sequentialGamePlayViewModel.Retry();

            //TODO refactor 
            StackLayout.IsVisible = true;
            Failed.IsVisible = false;


            PlayLayout.IsVisible = false;
            Content = StackLayout;

            NextRound();
        }
    }
}