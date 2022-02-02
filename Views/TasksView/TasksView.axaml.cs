using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YourTasks.Views
{
    public partial class TasksView : UserControl
    {
        public TasksView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}