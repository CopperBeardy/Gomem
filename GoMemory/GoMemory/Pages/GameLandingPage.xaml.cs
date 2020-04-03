using GoMemory.Enums;
using GoMemory.Helpers;
using GoMemory.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace GoMemory.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameLandingPage : ContentPage
    {
        public GameType GameType { get; set; }
        public ResumeModel ResumeModel { get; set; }

        public GameLandingPage(string buttonText)
        {
            InitializeComponent();
            Title = buttonText;
            SetGameType(buttonText);

        }

        private void SetGameType(string buttonText)
        {

            switch (buttonText)
            {
                case "What you see":
                    GameType = GameType.Guess;
                    break;
                case "Sequential":
                    GameType = GameType.Sequential;
                    break;
                default:
                    GameType = GameType.ColourComplex;
                    break;
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            CheckResume();
        }

        /// <summary>
        /// check to see if there is a saved game
        /// </summary>
        public void CheckResume()
        {
            ResumeModel = ResumeHelper.CheckResume(GameType);
            if (ResumeModel != null)
            {
                ResumeBtn.IsEnabled = true;

            }
        }

        public void ResumeBtn_OnClicked(object sender, EventArgs e)
        {
            SetGamePlay(ResumeModel.Difficulty, ResumeModel);
        }

    
        public async void StatusBtn_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatusPage(GameType));       
        }


        public async void RulesBtn_OnClicked(object sender, EventArgs e)
        {
              await Navigation.PushAsync(new RulesPage(GameType));
        
        }


        /// <summary>
        /// setting the difficulty of  the games
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetGame_OnClicked(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if(clickedButton == null)
                return;


            switch (clickedButton.Text)
            {
                case "Easy":
                    SetGamePlay(Difficulty.Easy);
                    break;
                case "Normal":
                    SetGamePlay(Difficulty.Normal);
                    break;
                default:
                    SetGamePlay(Difficulty.Hard);
                    break;
            }


        }


        //set game play style
        public async void SetGamePlay(Difficulty difficulty, ResumeModel resume = null)
        {
            switch (GameType)
            {
                case GameType.Guess:
                    await Navigation.PushAsync(new WhatYouSeeGamePlayPage(difficulty, GameType, resume));
                    break;
                case GameType.ColourComplex:
                    await Navigation.PushAsync(new ColourComplexGamePlayPage(difficulty, GameType, resume));
                    break;
                default:
                    await Navigation.PushAsync(new SequentialGamePlayPage(difficulty, GameType, resume));
                    break;
            }
        }
    }
}