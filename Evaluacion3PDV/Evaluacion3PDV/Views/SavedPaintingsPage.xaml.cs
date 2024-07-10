using Evaluacion3PDV.ViewModels;

namespace Evaluacion3PDV.Views
{
    public partial class SavedPaintingsPage : ContentPage
    {
        public SavedPaintingsPage()
        {
            InitializeComponent();
            BindingContext = new DVSavedPaintingsViewModel();
        }
    }
}
