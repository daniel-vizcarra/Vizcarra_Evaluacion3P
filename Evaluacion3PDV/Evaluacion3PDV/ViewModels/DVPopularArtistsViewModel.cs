using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Evaluacion3PDV.Models;
using Evaluacion3PDV.Services;
using Microsoft.Maui.Controls;

namespace Evaluacion3PDV.ViewModels
{
    public class DVPopularArtistsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DVArtist> Artists { get; set; }
        public Command LoadArtistsCommand { get; }
        public Command<DVArtist> SaveArtistCommand { get; }

        public DVPopularArtistsViewModel()
        {
            Artists = new ObservableCollection<DVArtist>();
            LoadArtistsCommand = new Command(async () => await ExecuteLoadArtistsCommand());
            SaveArtistCommand = new Command<DVArtist>(async (artist) => await SaveArtist(artist));
            LoadArtistsCommand.Execute(null); // Cargar datos al iniciar el ViewModel
        }

        async Task ExecuteLoadArtistsCommand()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetStringAsync("https://www.wikiart.org/en/app/api/popularartists?json=1");
                var artists = JsonConvert.DeserializeObject<List<DVArtist>>(response);
                var topArtists = artists.Take(20); // Limitar a los primeros 20 artistas
                foreach (var artist in topArtists)
                {
                    Artists.Add(artist);
                }
            }
            catch (JsonReaderException ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error en JSON: {ex.Message}", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error inesperado: {ex.Message}", "OK");
            }
        }

        async Task SaveArtist(DVArtist artist)
        {
            var existingArtists = await App.Database.GetArtistsAsync();
            if (existingArtists.Any(a => a.ArtistName == artist.ArtistName))
            {
                await Shell.Current.DisplayAlert("Error", "¡El artista ya está guardado!", "OK");
                return;
            }

            await App.Database.SaveArtistAsync(artist);
            MessagingCenter.Send(this, "UpdateSavedArtists"); // Enviar un mensaje para actualizar la lista de artistas guardados
            await Shell.Current.DisplayAlert("Guardado", "¡El artista ha sido guardado!", "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
