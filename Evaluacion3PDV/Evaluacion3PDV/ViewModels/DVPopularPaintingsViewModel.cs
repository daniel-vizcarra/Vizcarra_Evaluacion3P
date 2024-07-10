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
            LoadPaintingsCommand.Execute(null); // Cargar datos al iniciar el ViewModel
        }

        async Task ExecuteLoadPaintingsCommand()
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("https://www.wikiart.org/en/popular-paintings?json=1&page=1");
            var paintings = JsonConvert.DeserializeObject<List<DVPainting>>(response);
            var topPaintings = paintings.Take(20); // Limitar a las primeras 20 pinturas
            foreach (var painting in topPaintings)
            {
                Paintings.Add(painting);
            }
        }

        async Task SavePainting(DVPainting painting)
        {
            var existingPaintings = await App.Database.GetPaintingsAsync();
            if (existingPaintings.Any(p => p.Title == painting.Title))
            {
                await Shell.Current.DisplayAlert("Error", "¡La pintura ya está guardada!", "OK");
                return;
            }

            await App.Database.SavePaintingAsync(painting);
            MessagingCenter.Send(this, "UpdateSavedPaintings"); // Enviar un mensaje para actualizar la lista de pinturas guardadas
            await Shell.Current.DisplayAlert("Guardada", "¡La pintura ha sido guardada!", "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
