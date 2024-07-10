using Evaluacion3PDV.ViewModels;

namespace Evaluacion3PDV.Views
{
    public partial class PopularPaintingsPage : ContentPage
    {
        public PopularPaintingsPage()
        {
            InitializeComponent();
            BindingContext = new DVPopularPaintingsViewModel();
        }
    }
}
