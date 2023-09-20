using System;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SQLiteDemo
{
    public class MyDatabase
    {
        readonly SQLiteAsyncConnection dbConnection;

        public MyDatabase()
        {
            string databasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "ToDoDatabase.db");
            Console.WriteLine($"+++++++Database Path: {databasePath}");

            dbConnection = new SQLiteAsyncConnection(databasePath);

            dbConnection.CreateTableAsync<ToDoItem>().Wait();

            Console.WriteLine("$+++++++ Database table created!");
        }

        // CRUD operations

        // Add a new item to the database
        public async Task<int> AddItem(ToDoItem itemToAdd)
        {
            int numRowsAdded = await dbConnection.InsertAsync(itemToAdd);
            return numRowsAdded;
        }

        // Update an existing item in the database
        public async Task<int> UpdateItem(ToDoItem itemToUpdate)
        {
            int numRowsUpdated = await dbConnection.UpdateAsync(itemToUpdate);
            return numRowsUpdated;
        }

        // Delete an item from the database by its ID
        public async Task<int> DeleteItem(int itemId)
        {
            int numRowsDeleted = await dbConnection.DeleteAsync<ToDoItem>(itemId);
            return numRowsDeleted;
        }

        // Get an item from the database by its ID
        public async Task<ToDoItem> GetItemById(int itemId)
        {
            return await dbConnection.Table<ToDoItem>().Where(i => i.Id == itemId).FirstOrDefaultAsync();
        }

        // Get all items from the database
        public async Task<List<ToDoItem>> GetAllItems()
        {
            return await dbConnection.Table<ToDoItem>().ToListAsync();
        }

        // Get items from the database based on priority
        public async Task<List<ToDoItem>> GetItemsByPriority(bool isHighPriority)
        {
            return await dbConnection.Table<ToDoItem>().Where(i => i.IsHighPriority == isHighPriority).ToListAsync();
        }
    }
}
