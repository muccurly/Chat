using SoilesuX.Models;
using SoilesuX.View;
using SoilesuX.Views.LoginPage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SoilesuX
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App()
        {
            InitializeComponent();
            if (String.IsNullOrEmpty(MyCookie.Cookie))
            {
                MainPage = new NavigationPage(new TabbedForm());
            }
            else
            {
               
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
