using Millsnet.DvlApp.Views;

namespace Millsnet.DvlApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
        Routing.RegisterRoute("EditVehicle", typeof(EditVehicleView));
        InitializeComponent();
	}
}
