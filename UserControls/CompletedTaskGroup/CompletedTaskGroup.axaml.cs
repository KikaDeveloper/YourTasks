using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YourTasks.TaskGroup
{
    public partial class CompletedTaskGroup : UserControl
    {
        public CompletedTaskGroup()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}