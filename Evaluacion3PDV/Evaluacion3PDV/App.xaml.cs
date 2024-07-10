using System.IO;
using Evaluacion3PDV.Services;
using Xamarin.Forms;

namespace Evaluacion3PDV
{
    public partial class App : Application
    {
        static DVDatabase database;

        public static DVDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new DVDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DVWikiArt.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
