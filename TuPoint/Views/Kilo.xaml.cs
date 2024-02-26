using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TuPoint.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuPoint.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Kilo : ContentPage
    {
        string rutaImagen, elementoId, lat, lon, user_id;
        public Kilo(string elementoSeleccionado)
        {
            InitializeComponent();
            elementoId = elementoSeleccionado;
            obtenerUbicacion();
            obtenerIdUsuarioActual();
            VerificarPermisos();
        }

        private async void obtenerIdUsuarioActual()
        {
            User user = await App.SQLiteDb.GetFirstUserAsync();
            user_id = user.cod_visitante.ToString();
        }

        private async void obtenerUbicacion()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default));

                if (location != null)
                {
                    double latitud = location.Latitude;
                    double longitud = location.Longitude;

                    // Ahora puedes usar latitud y longitud según tus necesidades
                    Console.WriteLine($"Ubicación actual: {latitud}, {longitud}");

                    lat = latitud.ToString();
                    lon = longitud.ToString();
                }
            }
            catch (FeatureNotSupportedException)
            {
                // La geolocalización no es compatible en este dispositivo
            }
            catch (PermissionException)
            {
                SolicitarPermisoUbicacion();
            }
            catch (Exception ex)
            {
                // Manejar otros errores
                Console.WriteLine($"Error al obtener la ubicación: {ex.Message}");
            }
        }

        private async void SolicitarPermisoUbicacion()
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (status == PermissionStatus.Granted)
                {
                    // El permiso fue otorgado, ahora puedes llamar a ObtenerUbicacionActual()
                    obtenerUbicacion();
                }
                else
                {
                    // El usuario denegó el permiso
                    // Puedes manejar esta situación según tus necesidades
                }
            }
            catch (Exception ex)
            {
                // Manejar errores
                Console.WriteLine($"Error al solicitar el permiso: {ex.Message}");
            }
        }

        private async void VerificarPermisos()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

                if (status != PermissionStatus.Granted)
                {
                    var newStatus = await Permissions.RequestAsync<Permissions.StorageRead>();

                    if (newStatus != PermissionStatus.Granted)
                    {
                        // El usuario no concedió los permisos, puedes manejarlo adecuadamente.
                        await DisplayAlert("Error", "La aplicación no tiene permisos para leer archivos en el almacenamiento externo.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al verificar los permisos
                Console.WriteLine($"Error al verificar permisos: {ex.Message}");
            }
        }

        private async void CargarImagen_Clicked(object sender, EventArgs e)
        {
            try
            {
                var resultado = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Seleccionar una imagen"
                });

                if (resultado != null)
                {
                    rutaImagen = resultado.FullPath;
                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar la imagen: {ex.Message}");
            }
        }

        private async void RegistrarKilos_Clicked(object sender, EventArgs e)
        {
            // Aquí puedes agregar la lógica para registrar los kilos
            // Puedes acceder al valor ingresado en el Entry txtKilos.Text
            //string kilosComprobados = txtKilos.Text;

            try
            {
                string kilos = txtKilos.Text;

                if (string.IsNullOrEmpty(rutaImagen))
                {
                    await DisplayAlert("Error", "Debe seleccionar una imagen.", "OK");
                    return;
                }

                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // URL de tu endpoint en el servidor para manejar la carga de datos
                    string url = "https://tupoint.com/apk/mobile/insertar_kilo.php";

                    // Crear un objeto MultipartFormDataContent para incluir datos de texto e imagen
                    MultipartFormDataContent content = new MultipartFormDataContent();

                    // Agregar el valor de kilos como un campo de formulario
                    content.Add(new StringContent(kilos), "kilo");
                    content.Add(new StringContent(elementoId), "elementoId");
                    content.Add(new StringContent(lat), "lat");
                    content.Add(new StringContent(lon), "lon");
                    content.Add(new StringContent(user_id), "cod_visitante");

                    // Leer la imagen en bytes
                    byte[] imagenBytes = File.ReadAllBytes(rutaImagen);

                    // Agregar la imagen como un contenido de archivo
                    content.Add(new ByteArrayContent(imagenBytes), "imagen", "imagen.png");

                    // Realizar la solicitud POST
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Verificar si la solicitud fue exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Manejar la respuesta del servidor si es necesario
                        string respuestaServidor = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("Éxito", $"Respuesta del servidor: {respuestaServidor}", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", $"Error en la solicitud: {response.ReasonPhrase}", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar los kilos: {ex.Message}");
            }
        }
    }
}