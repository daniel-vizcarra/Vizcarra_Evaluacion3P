using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Evaluacion3PDV.Models;
using Evaluacion3PDV.Services;
using Microsoft.Maui.Controls;

namespace Evaluacion3PDV.ViewModels
{
    public class DVSavedPaintingsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DVPainting> SavedPaintings { get; set; }
        public Command LoadSavedPaintingsCommand { get; }
        public Command<DVPainting> DeletePaintingCommand { get; }

        public DVSavedPaintingsViewModel()
        {
            SavedPaintings = new ObservableCollection<DVPainting>();
            LoadSavedPaintingsCommand = new Command(async () => await ExecuteLoadSavedPaintingsCommand());
            DeletePaintingCommand = new Command<DVPainting>(async (painting) => await DeletePainting(painting));
            LoadSavedPaintingsCommand.Execute(null); // Cargar datos al iniciar el ViewModel

            // Escuchar cambios en las pinturas guardadas
            MessagingCenter.Subscribe<DVPopularPaintingsViewModel>(this, "UpdateSavedPaintings", async (sender) =>
            {
                await ExecuteLoadSavedPaintingsCommand();
            });
        }

        async Task ExecuteLoadSavedPaintingsCommand()
        {
            var paintings = await App.Database.GetPaintingsAsync();
            SavedPaintings.Clear();
            foreach (var painting in paintings)
            {
                SavedPaintings.Add(painting);
            }
        }

        async Task DeletePainting(DVPainting painting)
        {
            await App.Database.DeletePaintingAsync(painting);
            SavedPaintings.Remove(painting);
            await Shell.Current.DisplayAlert("Deleted", "The painting has been deleted!", "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
