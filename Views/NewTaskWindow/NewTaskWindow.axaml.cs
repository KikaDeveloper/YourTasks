using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using YourTasks.ViewModels;
using ReactiveUI;

namespace YourTasks.Views
{
    public partial class NewTaskWindow : ReactiveWindow<NewTaskViewModel>
    {
        public NewTaskWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            this.WhenActivated(d => ViewModel!.AddNewTaskCommand.Subscribe(Close));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}