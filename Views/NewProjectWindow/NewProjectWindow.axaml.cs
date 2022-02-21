using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using YourTasks.ViewModels;
using ReactiveUI;

namespace YourTasks.Views
{
    public partial class NewProjectWindow : ReactiveWindow<NewProjectViewModel>
    {
        public NewProjectWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(d => ViewModel!.AddProjectCommand.Subscribe(Close));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}