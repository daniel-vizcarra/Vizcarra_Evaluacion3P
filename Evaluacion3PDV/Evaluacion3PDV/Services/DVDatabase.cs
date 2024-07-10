using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evaluacion3PDV.Models;

namespace Evaluacion3PDV.Services
{
    public class DVDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public DVDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<DVPainting>().Wait();
            _database.CreateTableAsync<DVArtist>().Wait();
        }

        public Task<List<DVPainting>> GetPaintingsAsync() => _database.Table<DVPainting>().ToListAsync();
        public Task<int> SavePaintingAsync(DVPainting painting) => _database.InsertAsync(painting);
        public Task<List<DVArtist>> GetArtistsAsync() => _database.Table<DVArtist>().ToListAsync();
        public Task<int> SaveArtistAsync(DVArtist artist) => _database.InsertAsync(artist);
    }
}
