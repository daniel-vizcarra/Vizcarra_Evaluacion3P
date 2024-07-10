using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Evaluacion3PDV.Models;
using Evaluacion3PDV.Services;
using Microsoft.Maui.Controls;

namespace Evaluacion3PDV.ViewModels
{
    public class DVSavedArtistsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DVArtist> SavedArtists { get; set; }
        public Command LoadSavedArtistsCommand { get; }
        public Command<DVArtist> DeleteArtistCommand { get; }

        public DVSavedArtistsViewModel()
        {
            SavedArtists = new ObservableCollection<DVArtist>();
            LoadSavedArtistsCommand = new Command(async () => await ExecuteLoadSavedArtistsCommand());
            DeleteArtistCommand = new Command<DVArtist>(async (artist) => await DeleteArtist(artist));
            LoadSavedArtistsCommand.Execute(null); // Cargar datos al iniciar el ViewModel

            // Escuchar cambios en los artistas guardados
            MessagingCenter.Subscribe<DVPopularArtistsViewModel>(this, "UpdateSavedArtists", async (sender) =>
            {
                await ExecuteLoadSavedArtistsCommand();
            });
        }

        async Task ExecuteLoadSavedArtistsCommand()
        {
            var artists = await App.Database.GetArtistsAsync();
            SavedArtists.Clear();
            foreach (var artist in artists)
            {
                SavedArtists.Add(artist);
            }
        }

        async Task DeleteArtist(DVArtist artist)
        {
            await App.Database.DeleteArtistAsync(artist);
            SavedArtists.Remove(artist);
            await Shell.Current.DisplayAlert("Eliminado", "¡El artista ha sido eliminado!", "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
