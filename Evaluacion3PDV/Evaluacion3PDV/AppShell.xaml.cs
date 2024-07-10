using Microsoft.Maui.Controls;

namespace Evaluacion3PDV
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.PopularPaintingsPage), typeof(Views.PopularPaintingsPage));
            Routing.RegisterRoute(nameof(Views.SavedPaintingsPage), typeof(Views.SavedPaintingsPage));
            Routing.RegisterRoute(nameof(Views.PopularArtistsPage), typeof(Views.PopularArtistsPage));
            Routing.RegisterRoute(nameof(Views.SavedArtistsPage), typeof(Views.SavedArtistsPage));
        }
    }
}
