using APPAsissteXamarin.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace APPAsissteXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}