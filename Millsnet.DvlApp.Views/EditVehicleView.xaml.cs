using Millsnet.DvlApp.ViewModels;

namespace Millsnet.DvlApp.Views;

public partial class EditVehicleView : ContentPage
{
	public EditVehicleView(EditVehicleViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}