using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TuPoint.Models;

namespace TuPoint.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection conn;
        public SQLiteHelper(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<User>().Wait();
        }

        public Task<int> createUserAsync(User user) {
            return conn.InsertAsync(user);
        }

        public Task<User> GetFirstUserAsync()
        {
            return conn.Table<User>().FirstOrDefaultAsync();
        }
    }
}
