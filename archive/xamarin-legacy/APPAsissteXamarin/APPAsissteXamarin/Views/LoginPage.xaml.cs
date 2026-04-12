using APPAsissteXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPAsissteXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
           
        }

        private async void Ingresar(object sender, EventArgs e)
        {
            if (idCorreo.Text != null && idClave.Text != null)
            {
                await DisplayAlert("HOLA", "Bienvenido", "OK");
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
               

            }
            else {
                await DisplayAlert("Campos vacios", "Ingresar sus credenciales en los campos faltantes", "OK");
            }
        }
    }
}