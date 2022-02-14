using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YourTasks.UserControls
{
    public partial class TaskPresenter : UserControl
    {
        public TaskPresenter()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}