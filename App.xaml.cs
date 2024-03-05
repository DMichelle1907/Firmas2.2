using Ejercicio2_2.Models;
using Ejercicio2_2.Views;

namespace Ejercicio2_2
{
    public partial class App : Application
    {
            static FirmaControllers firma;

            public static FirmaControllers DataBase
            {
                get
                {
                    if (firma == null)
                    {
                        firma = new FirmaControllers();
                    }
                    return firma;
                }
            }

        
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Lista());
        }
    }
}