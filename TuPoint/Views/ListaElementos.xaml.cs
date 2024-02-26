using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuPoint.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuPoint.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaElementos : ContentPage
    {
        public ListaElementos()
        {
            InitializeComponent();

            var listaElementos = new List<ElementoModelo>
            {
                new ElementoModelo { Id = 1, Nombre = "Papel color y blanco" },
                new ElementoModelo { Id = 2, Nombre = "Carton" },
                new ElementoModelo { Id = 3, Nombre = "Bolsas Film" },
                new ElementoModelo { Id = 4, Nombre = "Vidrio" },
                new ElementoModelo { Id = 5, Nombre = "Metal" },
                new ElementoModelo { Id = 6, Nombre = "Plastico pet" },
                new ElementoModelo { Id = 7, Nombre = "Caucho" },
                new ElementoModelo { Id = 8, Nombre = "Plastico doble" },
                new ElementoModelo { Id = 9, Nombre = "Organico" },
                new ElementoModelo { Id = 10, Nombre = "Residuos electronicos" },
                new ElementoModelo { Id = 11, Nombre = "Aceite" },
                new ElementoModelo { Id = 12, Nombre = "Ropa y zapatos" },
                new ElementoModelo { Id = 13, Nombre = "Madera" },
                new ElementoModelo { Id = 14, Nombre = "Libros" },
                new ElementoModelo { Id = 15, Nombre = "Elementos peligrosos" },
                new ElementoModelo { Id = 16, Nombre = "Tetrapack" },
                new ElementoModelo { Id = 17, Nombre = "Juguetes" }
            };
            // Asignar la lista al ItemSource del ListView
            miListView.ItemsSource = listaElementos;

        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is ElementoModelo selectedItem)
            {
                // Acceder al modelo de datos del elemento seleccionado y pasar el parámetro a la nueva vista
                string elementoSeleccionado = selectedItem.Id.ToString();

                // Abrir la nueva vista y pasar el parámetro
                Navigation.PushAsync(new Kilo(elementoSeleccionado));

                // Desmarcar la selección en el ListView
                miListView.SelectedItem = null;
            }
        }
    }
}