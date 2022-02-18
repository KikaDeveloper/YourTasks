using SQLite;
using System.Collections.Generic;
using YourTasks.Models;

namespace YourTasks.Services
{
    public class AppRepository
    {
        private static AppRepository? instance;
        public static AppRepository Instance
        {
            get {
                if(instance == null)
                    instance = new AppRepository();
                return instance;  
            }
        }

        private SQLiteAsyncConnection connection;
        protected AppRepository()
        {
            connection = new SQLiteAsyncConnection("tasks.db");
            EnsureCreateTables().GetAwaiter();
        }

        private async System.Threading.Tasks.Task EnsureCreateTables()
        {
            await connection.CreateTableAsync<YourTasks.Models.Task>();
            await connection.CreateTableAsync<Project>();
            await connection.CreateTableAsync<SubTask>();
        }

        public async System.Threading.Tasks.Task InsertEntity<TEntity>(TEntity entity) where TEntity : class
        {
            await connection.InsertAsync(entity);
        }

        public async System.Threading.Tasks.Task DeleteEntity<TEntity>(TEntity entity) where TEntity : class
        {
            await connection.DeleteAsync(entity);
        }

        public async System.Threading.Tasks.Task UpdateEntity<TEntity>(TEntity entity) where TEntity : class 
        {
            await connection.UpdateAsync(entity);
        }

        public async System.Threading.Tasks.Task<IEnumerable<TEntity>> GetAllEntites<TEntity>() where TEntity : new()
        {
            return await connection.Table<TEntity>().ToListAsync();
        }

    }
}