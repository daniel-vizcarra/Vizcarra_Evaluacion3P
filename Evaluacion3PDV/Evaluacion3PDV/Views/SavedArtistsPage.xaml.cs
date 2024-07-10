using Evaluacion3PDV.ViewModels;

namespace Evaluacion3PDV.Views
{
    public partial class SavedArtistsPage : ContentPage
    {
        public SavedArtistsPage()
        {
            InitializeComponent();
            BindingContext = new DVSavedArtistsViewModel();
        }
    }
}
