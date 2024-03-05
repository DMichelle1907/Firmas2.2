namespace Ejercicio2_2.Views;

public partial class Lista : ContentPage
{
	public Lista()
	{
		InitializeComponent();
	}

    private async void toolmenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        listapersonas.ItemsSource = await App.BaseDatos.ObtenerEmpleado();
    }
}