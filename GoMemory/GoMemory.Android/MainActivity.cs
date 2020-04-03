using Android.App;
using Android.Content.PM;
using Android.OS;
using GoMemory.Droid.DataAccess;
using System;

namespace GoMemory.Droid
{
    [Activity(Label = "GoMemory", Icon = "@mipmap/icon", Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
     ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            string dbPath = SqliteDbConnectionHelper.GetLocalDbPath("GoMemory.db3");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(dbPath));
        }
    }



}

