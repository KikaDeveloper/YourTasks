using SQLite;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            // System.Threading.Tasks.Task.Run(async() => await EnsureCreateTables());
        }

        public async System.Threading.Tasks.Task CloseConnection()
            => await connection.CloseAsync();

        public async System.Threading.Tasks.Task EnsureCreateTables()
        {
            await connection.CreateTableAsync<Project>();
            await connection.CreateTableAsync<YourTasks.Models.Task>();
            await connection.CreateTableAsync<SubTask>();
        }

        public async System.Threading.Tasks.Task InsertEntity<TEntity>(TEntity entity) where TEntity : class
            => await connection.InsertAsync(entity);

        public async System.Threading.Tasks.Task DeleteEntity<TEntity>(TEntity entity) where TEntity : class
            => await connection.DeleteAsync(entity);

        public async System.Threading.Tasks.Task UpdateEntity<TEntity>(TEntity entity) where TEntity : class 
            => await connection.UpdateAsync(entity);

        public async System.Threading.Tasks.Task<List<TEntity>> GetAllEntites<TEntity>() where TEntity : new()
            => await connection.Table<TEntity>().ToListAsync();

        public async System.Threading.Tasks.Task CascadeDeleteProject(Project project)
        {
            foreach(var task in project.Tasks!)
            {
                foreach(var subTask in task.SubTasks!)
                    await DeleteEntity<SubTask>(subTask);

                await DeleteEntity<Task>(task);
            }
            await DeleteEntity<Project>(project);
        }

        public async System.Threading.Tasks.Task<List<Project>> GetAllProjects()
        {
            var subTasks = await GetAllEntites<SubTask>();
            var tasks = await GetAllEntites<Task>();
            var projects = await GetAllEntites<Project>();
            
            foreach(var project in projects)
            {
                var projectTasks = tasks.Where<Task>(task => task.ProjectId == project.Id);
                foreach(var task in projectTasks)
                {
                    task.SubTasks = new ObservableCollection<SubTask>(subTasks.Where(subTask => subTask.TaskId == task.Id));
                }
                project.Tasks = new ObservableCollection<Task>(projectTasks);
            }
            return projects;
        }

    }
}