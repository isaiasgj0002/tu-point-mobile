using Android.OS;
using Newtonsoft.Json;
using Org.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public ICommand CallCommand { get; set; }
        public Main()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            ObtenerUbicacionActual();
            LoadData();
            _httpService = new HttpService();
            CallCommand = new Command<string>(OnCallCommandExecute);
            BindingContext = this;
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

        private async void LoadData()
        {
            User user = await App.SQLiteDb.GetFirstUserAsync();
            Console.WriteLine(user);
            if (user != null)
            {
                username.Text = "Usuario: "+user.nombre_apellido.ToString();
                saldo.Text = "Saldo: S/."+user.monedas_tupoint.ToString();
            }
        }

        private void OnCallCommandExecute(string phoneNumber)
        {
            try
            {
                // Llama al marcador telefónico con el número proporcionado
                PhoneDialer.Open(phoneNumber);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Funcionalidad no soportada en este dispositivo
                DisplayAlert("Error", "La llamada telefónica no es compatible en este dispositivo.", "OK");
            }
            catch (Exception ex)
            {
                // Ocurrió un error inesperado
                DisplayAlert("Error", "Ocurrió un error al intentar realizar la llamada.", "OK");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            // Evita que el usuario retroceda con el botón atrás
            return true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListaElementos());
        }
    }
}