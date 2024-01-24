using Android.OS;
using Newtonsoft.Json;
using Org.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuPoint.Http;
using TuPoint.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuPoint.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : ContentPage
    {
        private readonly HttpService _httpService;
        public Main()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            ObtenerUbicacionActual();
            _httpService = new HttpService();
        }

        async void ObtenerUbicacionActual()
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

                    string result = await _httpService.GetCompanies(latitud.ToString(), longitud.ToString());
                    if(!string.IsNullOrEmpty(result))
                    {
                        // Deserializamos la cadena JSON a una lista de objetos
                        List<Company> empresas = JsonConvert.DeserializeObject<List<Company>>(result);
                    }
                    else
                    {
                        List<Company> empresas = new List<Company>();
                    }
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

        async void SolicitarPermisoUbicacion()
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (status == PermissionStatus.Granted)
                {
                    // El permiso fue otorgado, ahora puedes llamar a ObtenerUbicacionActual()
                    ObtenerUbicacionActual();
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

        protected override bool OnBackButtonPressed()
        {
            // Evita que el usuario retroceda con el botón atrás
            return true;
        }
    }
}