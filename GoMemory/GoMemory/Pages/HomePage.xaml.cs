using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace GoMemory.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
            InitializeComponent();
            Title = "Go Memory";
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if(!(sender is Button btn)) {
                return;
            }

            await Navigation.PushAsync(new GameLandingPage(btn.Text));
        }
    }
}