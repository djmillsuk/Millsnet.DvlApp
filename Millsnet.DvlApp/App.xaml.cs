using Millsnet.DvlApp.Interfaces;
using Millsnet.DvlApp.ViewModels;

namespace Millsnet.DvlApp;

public partial class App : Application
{
	public App(INavigationService navigationService)
	{
		InitializeComponent();

		MainPage = new AppShell();

		navigationService.Initialise();
		
		//navigationService.NavigateAsync<HomeViewModel>();
	}
}
