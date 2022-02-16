using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YourTasks.TaskGroup
{
    public partial class TotalTaskGroup : UserControl
    {
        public TotalTaskGroup()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}