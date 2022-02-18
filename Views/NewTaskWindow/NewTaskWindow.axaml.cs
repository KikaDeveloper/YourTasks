using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YourTasks.Views
{
    public partial class NewTaskWindow : Window
    {
        public NewTaskWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}