using Evaluacion3PDV.ViewModels;
using Microsoft.Maui.Controls;

namespace Evaluacion3PDV.Views
{
    public partial class PopularArtistsPage : ContentPage
    {
        public PopularArtistsPage()
        {
            InitializeComponent();
            BindingContext = new DVPopularArtistsViewModel();
        }
    }
}
