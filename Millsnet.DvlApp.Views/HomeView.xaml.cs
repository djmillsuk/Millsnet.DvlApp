using Millsnet.DvlApp.ViewModels;

namespace Millsnet.DvlApp.Views;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}