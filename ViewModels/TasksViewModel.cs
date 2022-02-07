using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class TasksViewModel : ViewModelBase
    {
        private ObservableCollection<TabItemViewModel>? _TabItemViewModels;
        private TabItemViewModel? _selectedTabItemViewModel;

        public ObservableCollection<TabItemViewModel> TabItemViewModels
        {
            get => _TabItemViewModels!;
            set => this.RaiseAndSetIfChanged(ref _TabItemViewModels, value);
        }

        public TabItemViewModel SelectedTabItemViewModel
        {
            get => _selectedTabItemViewModel!;
            set => this.RaiseAndSetIfChanged(ref _selectedTabItemViewModel, value);
        }

        public TasksViewModel()
        {
            TabItemViewModels = new ObservableCollection<TabItemViewModel>(){
                new TabItemViewModel(){
                    Project = new Project(){
                        Id = Guid.NewGuid(),
                        Name = "Project",
                        EllipseColor="Red"
                    },
                    TabContentViewModel = new TabContentViewModel()
                }
            };
        }
    }
}