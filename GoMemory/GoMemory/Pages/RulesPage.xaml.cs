using GoMemory.Enums;
using GoMemory.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoMemory.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RulesPage : ContentPage
    {
        private RulesViewModel _rulesViewModel;
        public RulesPage(GameType gameType)
        {

            //TODO refactor out game rules to seperate view components 
            //and import tha as needed

            InitializeComponent();
            BindingContext = _rulesViewModel = new RulesViewModel(gameType);           
            switch (gameType)
            {
                case GameType.Guess:
                    Title = "What you see Rules";
                    WhatYouSee.IsVisible = true;
                    break;
                case GameType.Sequential:
                    Title = "Sequential";
                    Sequential.IsVisible = true;
                    break;
                case GameType.ColourComplex:
                    Title = "Colour Complex";
                    ColourComplex.IsVisible = true;
                    break;
            }

        }
    }
}