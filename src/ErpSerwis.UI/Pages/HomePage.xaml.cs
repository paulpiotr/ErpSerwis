using ErpSerwis.Core.ViewModels.Home;
using 
/* Niescalona zmiana z projektu „ErpSerwis.UI (netcoreapp3.1)”
Przed:
using MvvmCross.Forms.Presenters.Attributes;
Po:
using ErpSerwis.Forms.Presenters.Attributes;
*/

/* Niescalona zmiana z projektu „ErpSerwis.UI (net5.0)”
Przed:
using MvvmCross.Forms.Presenters.Attributes;
Po:
using ErpSerwis.Forms.Presenters.Attributes;
*/
MvvmCross.Core.ViewModels.Home;
using MvvmCross.Forms.Presenters.Attributes;

/* Niescalona zmiana z projektu „ErpSerwis.UI (netcoreapp3.1)”
Przed:
using ErpSerwis.Core.ViewModels.Home;
Po:
using MvvmCross.Forms.Views;
*/

/* Niescalona zmiana z projektu „ErpSerwis.UI (net5.0)”
Przed:
using ErpSerwis.Core.ViewModels.Home;
Po:
using MvvmCross.Forms.Views;
*/
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ErpSerwis.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class HomePage : MvxContentPage<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                navigationPage.BarTextColor = Color.White;
                navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
            }
        }
    }
}
