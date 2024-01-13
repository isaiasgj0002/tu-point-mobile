using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuPoint.Http;
using Xamarin.Forms;

namespace TuPoint
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpService _httpService;
        public MainPage()
        {
            InitializeComponent();
            _httpService = new HttpService();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Implementa la lógica de inicio de sesión aquí
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            // TODO: Validar credenciales y realizar la acción correspondiente
            if (email.Length > 0 && password.Length > 0)
            {
                string result = await _httpService.LoginAsync(email, password);
            }

        }

        private void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // Implementa la lógica de registro aquí
            // Puedes navegar a la página de registro u otra acción según tus necesidades
        }
    }
}
