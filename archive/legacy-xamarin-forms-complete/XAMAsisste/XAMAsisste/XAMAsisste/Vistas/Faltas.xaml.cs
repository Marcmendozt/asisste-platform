

using Splat;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using XAMAsisste.VistaModelos;

namespace XAMAsisste.Vistas
{

    public partial class Faltas : ContentPage
    {
        public Faltas()
        {
        
            InitializeComponent();
            BindingContext = IFaltasVistaModelo;
        }



        internal FaltasVistaModelo IFaltasVistaModelo { get; set; } = Locator.Current.GetService<FaltasVistaModelo>();



        protected override async void OnAppearing()
        {
           
            base.OnAppearing();
           var Valor = await IFaltasVistaModelo.VInicio();
            if (Valor == true)
            {
                btnBuscar.IsEnabled = false;
                btnEnviar.IsEnabled = false;
                await DisplayAlert("MENSAJE","SOLO PUEDE ENVIAR UN ARCHIVO DE FALTA POR DÍA","OK");;
            }
            else {
                btnBuscar.IsEnabled = true;
                btnEnviar.IsEnabled = true;
            }

        }

    }
}