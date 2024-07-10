using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Evaluacion3PDV.Models;
using Evaluacion3PDV.Services;

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
        }

        async Task ExecuteLoadArtistsCommand()
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("https://www.wikiart.org/en/app/api/popularartists?json=1");
            var artists = JsonConvert.DeserializeObject<List<DVArtist>>(response);
            foreach (var artist in artists)
            {
                Artists.Add(artist);
            }
        }

        async Task SaveArtist(DVArtist artist)
        {
            await App.Database.SaveArtistAsync(artist);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
