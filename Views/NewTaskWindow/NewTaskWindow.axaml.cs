using System;
using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using YourTasks.ViewModels;
using ReactiveUI;

namespace YourTasks.Views
{
    public partial class NewTaskWindow : ReactiveWindow<NewTaskWindowViewModel>
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