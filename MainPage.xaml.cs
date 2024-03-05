using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Ejercicio2_2.Models;
using Ejercicio2_2.Views;
using SQLite;
using System.Collections.ObjectModel;

namespace Ejercicio2_2
{
    public partial class MainPage : ContentPage
    {
        FirmaControllers controller;
        // Controllers.RegistroController registro;
        public MainPage()
        {
            controller = new FirmaControllers();
            InitializeComponent();
            var drawingView = new DrawingView
            {
                Lines = new ObservableCollection<IDrawingLine>(),
                LineColor = Colors.Red,
                LineWidth = 5
            };
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Verificar que todos los campos estén llenos
                if (string.IsNullOrWhiteSpace(Nombre.Text) || string.IsNullOrWhiteSpace(Descripcion.Text))
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

                Models.Firmas Firmas = new Models.Firmas
                {
                    nombre = Nombre.Text,
                    descripcion = Descripcion.Text,
                    img= imagenBytes
                };
                if (await this.controller.AgregarFirma(Firmas) > 0)
                {
                    await DisplayAlert("Éxito", "Registro guardado exitosamente.", "OK");

                }
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
            Nombre.Text = "";
            Descripcion.Text = "";
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