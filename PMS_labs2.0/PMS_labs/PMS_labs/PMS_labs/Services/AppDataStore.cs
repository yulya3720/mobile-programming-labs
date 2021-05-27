using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PMS_labs.Data;
using PMS_labs.Models;
using SQLite;
using System.Linq;

namespace PMS_labs.Services
{
    public class AppDataStore
    {
        private string _dbPath;
        private SQLiteAsyncConnection _db;
    
        public static async Task<AppDataStore> Create()
        {
            var ds = new AppDataStore();
            await ds.Configure();
            return ds;
        }
        private AppDataStore()
        {
            _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Database.db");
            _db = new SQLiteAsyncConnection(_dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache | SQLiteOpenFlags.ReadWrite);
        }
        private async Task Configure()
        {
            await _db.CreateTablesAsync<Movie, MovieInfo, RemoteImage>();
        }
        public async Task InsertMovieAsync(Movie item)
        {
            await _db.InsertOrReplaceAsync(item);
        }
        public async Task InsertAllMoviesAsync(IEnumerable<Movie> items)
        {
            foreach (var b in items)
            {
                await InsertMovieAsync(b);
            }
        }

        public async Task InsertDetailsAsync(MovieInfo item)
        {
            await _db.InsertOrReplaceAsync(item);
        }

        public async Task InsertImageAsync(RemoteImage item)
        {
            await _db.InsertOrReplaceAsync(item);
        }

        public async Task InsertAllImagesAsync(IEnumerable<RemoteImage> items)
        {
            foreach (var i in items)
            {
                await InsertImageAsync(i);
            }
        }

        public async Task<Movie> GetMovieByIdAsync(string id)
        {
            try
            {
                return await _db.GetAsync<Movie>(id);
            }
            catch
            {
                return default;
            }
        }

        public async Task<MovieInfo> GetDetailsByIdAsync(string id)
        {
            try
            {
                return await _db.GetAsync<MovieInfo>(id);
            }
            catch
            {
                return default;
            }
        }

        public async Task<IEnumerable<Movie>> GetMoviesBySearchStringAsync(string search)
        {
            //return await _db.Table<Movie>().Where(b => b.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase)).ToArrayAsync();
            return await _db.Table<Movie>().Where(b => b.Title.ToLower().Contains(search.ToLower())).ToArrayAsync();
        }

        public async Task<IEnumerable<RemoteImage>> GetImagesBySearchStringAsync(string search)
        {
            return await _db.Table<RemoteImage>().Where(b => b.Search.ToLower().Contains(search.ToLower())).ToArrayAsync();
        }

    }

}
