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
    public class DVPopularPaintingsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DVPainting> Paintings { get; set; }
        public Command LoadPaintingsCommand { get; }
        public Command<DVPainting> SavePaintingCommand { get; }

        public DVPopularPaintingsViewModel()
        {
            Paintings = new ObservableCollection<DVPainting>();
            LoadPaintingsCommand = new Command(async () => await ExecuteLoadPaintingsCommand());
            SavePaintingCommand = new Command<DVPainting>(async (painting) => await SavePainting(painting));
        }

        async Task ExecuteLoadPaintingsCommand()
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("https://www.wikiart.org/en/popular-paintings?json=1&page=1");
            var paintings = JsonConvert.DeserializeObject<List<DVPainting>>(response);
            foreach (var painting in paintings)
            {
                Paintings.Add(painting);
            }
        }

        async Task SavePainting(DVPainting painting)
        {
            await App.Database.SavePaintingAsync(painting);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
