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
    public partial class Cargando : ContentPage
    {
        public Cargando()
        {
            InitializeComponent();
        }

        internal CargandoVistaModelo VistaModelo { get; set; } = Locator.Current.GetService<CargandoVistaModelo>();


        protected override void OnAppearing()
        {
            base.OnAppearing();
            VistaModelo.Inicio();
        }
    }
}