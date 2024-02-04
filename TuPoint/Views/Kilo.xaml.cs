using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuPoint.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Kilo : ContentPage
    {
        public Kilo()
        {
            InitializeComponent();
        }

        private void CargarImagen_Clicked(object sender, EventArgs e)
        {
            // Aquí puedes agregar la lógica para cargar una imagen
            // Puedes usar la biblioteca Xamarin.Essentials para interactuar con la galería o la cámara.
            // Ejemplo: https://docs.microsoft.com/en-us/xamarin/essentials/media-picker?tabs=android
        }

        private void RegistrarKilos_Clicked(object sender, EventArgs e)
        {
            // Aquí puedes agregar la lógica para registrar los kilos
            // Puedes acceder al valor ingresado en el Entry txtKilos.Text
            string kilosComprobados = txtKilos.Text;

            // Puedes implementar la lógica de registro aquí
        }
    }
}