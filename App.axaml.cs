using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using YourTasks.ViewModels;
using YourTasks.Views;
using YourTasks.Services;
using System.Threading.Tasks;

namespace YourTasks
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                AppRepository repo = AppRepository.Instance;
                Task.Run(async() => await repo.EnsureCreateTables());
                
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };

                DialogService.SetOwner(ref desktop);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}