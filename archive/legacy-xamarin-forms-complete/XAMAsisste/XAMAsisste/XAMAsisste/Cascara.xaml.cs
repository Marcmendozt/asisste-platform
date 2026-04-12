
using Android.App;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XAMAsisste.Vistas;

namespace XAMAsisste
{

    public partial class Cascara : Shell
    {
        public Cascara()
        {
            InitializeComponent();
            Routing.RegisterRoute("Principal/Login", typeof(Login));
            BindingContext = this;
        }


        [Obsolete]
        private void CerrarSesion(object sender, EventArgs e)
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }

     

       

    }
}