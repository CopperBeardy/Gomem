using GoMemory.Enums;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoMemory.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatusPage : ContentPage
    {
        public GameType GameType{ get; set; }

        public StatusPage(GameType gameType)
        {
            InitializeComponent();

            GameType = gameType;
            Title = " Stats";
            GetStatus();

        }


        public void GetStatus()
        {

            var templist = App.StatusRepository.GetGameStatus(GameType);

            foreach (var d in templist)
            {
                //    if (d == null) continue;
                switch (d.Difficulty)
                {
                    case Difficulty.Easy:
                        EasyLabel.Text = d.Level.ToString();

                        break;
                    case Difficulty.Normal:
                        NormalLabel.Text = d.Level.ToString();
                        break;
                    case Difficulty.Hard:
                        HardLabel.Text = d.Level.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

    }
}