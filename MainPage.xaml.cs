using CommunityToolkit.Maui.Views;
using Ejercicio2_2.Models;
using Ejercicio2_2.Views;
using SQLite;

namespace Ejercicio2_2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Verificar que todos los campos estén llenos
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    await MostrarAlerta("Error", "Por favor, complete todos los campos y firme antes de guardar.");
                    return;
                }

                byte[] imagenBytes = await ObtenerImagenDibujada();

                // Verificar que se haya dibujado algo en la firma
                if (imagenBytes == null || imagenBytes.Length == 0)
                {
                    await MostrarAlerta("Error", "Por favor, firme antes de guardar.");
                    return;
                }

                Constructor nuevoConstructor = new Constructor
                {
                    nombre = txtNombre.Text,
                    descripcion = txtDescripcion.Text,
                    imgM = imagenBytes
                };

                var resultado = await App.BaseDatos.EmpleadoGuardar(nuevoConstructor);

                LimpiarCampos();

                ((DrawingView)this.FindByName<DrawingView>("drawingView")).Clear();

                await MostrarAlerta("Éxito", "La información se guardó correctamente.");
            }
            catch (SQLiteException ex)
            {
                await MostrarAlerta("Error", $"Error de base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                await MostrarAlerta("Error", $"Hubo un error al intentar guardar: {ex.Message}");
            }
        }

        private async Task<byte[]> ObtenerImagenDibujada()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Stream imagenStream = await ((DrawingView)this.FindByName<DrawingView>("drawingView")).GetImageStream(200, 200);
                await imagenStream.CopyToAsync(stream);
                return stream.ToArray();
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
        }

        private async Task MostrarAlerta(string titulo, string mensaje)
        {
            await DisplayAlert(titulo, mensaje, "Aceptar");
        }


        private async void btnLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista());
        }
    }
}