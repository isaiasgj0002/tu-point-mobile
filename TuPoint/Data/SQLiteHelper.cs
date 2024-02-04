using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
