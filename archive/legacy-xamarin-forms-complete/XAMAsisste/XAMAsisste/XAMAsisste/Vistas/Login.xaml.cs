using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAMAsisste.VistaModelos;

namespace XAMAsisste.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            BindingContext = VistaModelo;
        }

        internal LoginVistaModelo VistaModelo { get; set; } = Locator.Current.GetService<LoginVistaModelo>();
         
        


        protected override bool OnBackButtonPressed()
        {
            return true;

        }
    }
}