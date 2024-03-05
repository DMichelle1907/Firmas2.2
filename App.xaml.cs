using Ejercicio2_2.Models;
using Ejercicio2_2.Views;

namespace Ejercicio2_2
{
    public partial class App : Application
    {
        static BasedeDatos basedatos;

        public static BasedeDatos BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new BasedeDatos(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "firmas.db3"));
                }
                return basedatos;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Lista());
        }
    }
}