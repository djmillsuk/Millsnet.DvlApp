using Millsnet.DvlApp.ViewModels;

namespace Millsnet.DvlApp.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView(SettingsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}