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

        public Task<List<DVPainting>> GetPaintingsAsync()
        {
            return _database.Table<DVPainting>().ToListAsync();
        }

        public Task<int> SavePaintingAsync(DVPainting painting)
        {
            return _database.InsertAsync(painting);
        }

        public Task<int> DeletePaintingAsync(DVPainting painting)
        {
            return _database.DeleteAsync(painting);
        }

        public Task<int> UpdatePaintingAsync(DVPainting painting)
        {
            return _database.UpdateAsync(painting);
        }

        public Task<List<DVArtist>> GetArtistsAsync()
        {
            return _database.Table<DVArtist>().ToListAsync();
        }

        public Task<int> SaveArtistAsync(DVArtist artist)
        {
            return _database.InsertAsync(artist);
        }

        public Task<int> DeleteArtistAsync(DVArtist artist)
        {
            return _database.DeleteAsync(artist);
        }

        public Task<int> UpdateArtistAsync(DVArtist artist)
        {
            return _database.UpdateAsync(artist);
        }
    }
}
